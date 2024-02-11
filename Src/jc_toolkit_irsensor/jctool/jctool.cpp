// Copyright (c) 2018 CTCaer. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include <cstdio>
#include <functional>
#include <memory>
#include <string>
// #define NOMINMAX
#include <Windows.h>

#include "jctool.h"
#include "ir_sensor.h"
#include "tune.h"
#include "FormJoy.h"
#include "hidapi.h"
#include "hidapi_log.h"

using namespace CppWinFormJoy;

#pragma comment(lib, "SetupAPI")

bool enable_traffic_dump = false;

hid_device *handle;
hid_device *handle_l;

s16 uint16_to_int16(u16 a) {
    s16 b;
    char* aPointer = (char*)&a, *bPointer = (char*)&b;
    memcpy(bPointer, aPointer, sizeof(a));
    return b;
}


u16 int16_to_uint16(s16 a) {
    u16 b;
    char* aPointer = (char*)&a, *bPointer = (char*)&b;
    memcpy(bPointer, aPointer, sizeof(a));
    return b;
}


u8 mcu_crc8_calc(u8 *buf, u8 size) {
    u8 crc8 = 0x0;

    for (int i = 0; i < size; ++i) {
        crc8 = mcu_crc8_table[(u8)(crc8 ^ buf[i])];
    }
    return crc8;
}

int get_spi_data(u32 offset, const u16 read_len, u8 *test_buf) {
    int res;
    u8 buf[49];
    int error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x10;
        pkt->spi_data.offset = offset;
        pkt->spi_data.size = read_len;
        res = hid_write(handle, buf, sizeof(buf));

        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if ((*(u16*)&buf[0xD] == 0x1090) && (*(uint32_t*)&buf[0xF] == offset))
                goto check_result;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 20)
            return 1;
    }
    check_result:
    if (res >= 0x14 + read_len) {
            for (int i = 0; i < read_len; i++) {
                test_buf[i] = buf[0x14 + i];
            }
    }
    
    return 0;
}


int write_spi_data(u32 offset, const u16 write_len, u8* test_buf) {
    int res;
    u8 buf[49];
    int error_writing = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x11;
        pkt->spi_data.offset = offset;
        pkt->spi_data.size = write_len;
        for (int i = 0; i < write_len; i++)
            buf[0x10 + i] = test_buf[i];

        res = hid_write(handle, buf, sizeof(buf));
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (*(u16*)&buf[0xD] == 0x1180)
                goto check_result;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_writing++;
        if (error_writing == 20)
            return 1;
    }
    check_result:
    return 0;
}


int get_device_info(u8* test_buf) {
    int res;
    u8 buf[49];
    int error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x02;
        res = hid_write(handle, buf, sizeof(buf));
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);        
            if (*(u16*)&buf[0xD] == 0x0282)
                goto check_result;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 20)
            break;
    }
    check_result:
    for (int i = 0; i < 0xA; i++) {
        test_buf[i] = buf[0xF + i];
    }

    return 0;
}

int dump_spi(const char *dev_name) {
    std::string file_dev_name = dev_name;
    int error_reading = 0;

    String^ filename_sys = gcnew String(file_dev_name.c_str());
    file_dev_name = "./" + file_dev_name;

    FILE *f;
    errno_t err;

    if ((err = fopen_s(&f, file_dev_name.c_str(), "wb")) != 0) {
        MessageBox::Show(L"Cannot open file " + filename_sys + L" for writing!\n\nError: " + err, L"Error opening file!", MessageBoxButtons::OK ,MessageBoxIcon::Exclamation);
        
        return 1;
    }

    int res;
    u8 buf[49];
    
    u16 read_len = 0x1d;
    u32 offset = 0x0;
    while (offset < 0x80000 && !cancel_spi_dump) {
        error_reading = 0;
        std::stringstream offset_label;
        offset_label << std::fixed << std::setprecision(2) << std::setfill(' ') << offset/1024.0f;
        offset_label << "KB of 512KB";
        FormJoy::myform1->label_progress->Text = gcnew String(offset_label.str().c_str());
        Application::DoEvents();

        while(1) {
            memset(buf, 0, sizeof(buf));
            auto hdr = (brcm_hdr *)buf;
            auto pkt = (brcm_cmd_01 *)(hdr + 1);
            hdr->cmd = 1;
            hdr->timer = timming_byte & 0xF;
            timming_byte++;
            pkt->subcmd = 0x10;
            pkt->spi_data.offset = offset;
            pkt->spi_data.size = read_len;
            res = hid_write(handle, buf, sizeof(buf));
            int retries = 0;
            while (1) {
                res = hid_read_timeout(handle, buf, sizeof(buf), 64);
                if ((*(u16*)&buf[0xD] == 0x1090) && (*(uint32_t*)&buf[0xF] == offset))
                    goto check_result;

                retries++;
                if (retries > 8 || res == 0)
                    break;
            }
            if (retries > 8)
                error_reading++;
            if (error_reading > 10) {
                fclose(f);
                return 1;
            }     
        }
        check_result:
        fwrite(buf + 0x14, read_len, 1, f);
        offset += read_len;
        if (offset == 0x7FFE6)
            read_len = 0x1A;
    }
    fclose(f);

    return 0;
}

int ir_sensor_auto_exposure(int white_pixels_percent) {
    int res;
    u8 buf[49];
    u16 new_exposure = 0;
    int old_exposure = (u16)FormJoy::myform1->numeric_IRExposure->Value;

    // Calculate new exposure;
    if (white_pixels_percent == 0)
        old_exposure += 10;
    else if (white_pixels_percent > 5)
        old_exposure -= (white_pixels_percent / 4) * 20;

    old_exposure = CLAMP(old_exposure, 0, 600);
    FormJoy::myform1->numeric_IRExposure->Value = old_exposure;
    new_exposure = old_exposure * 31200 / 1000;

    memset(buf, 0, sizeof(buf));
    auto hdr = (brcm_hdr *)buf;
    auto pkt = (brcm_cmd_01 *)(hdr + 1);
    hdr->cmd = 0x01;
    hdr->timer = timming_byte & 0xF;
    timming_byte++;
    pkt->subcmd = 0x21;

    pkt->subcmd_21_23_04.mcu_cmd    = 0x23; // Write register cmd
    pkt->subcmd_21_23_04.mcu_subcmd = 0x04; // Write register to IR mode subcmd
    pkt->subcmd_21_23_04.no_of_reg  = 0x03; // Number of registers to write. Max 9.

    pkt->subcmd_21_23_04.reg1_addr = 0x3001; // R: 0x0130 - Set Exposure time LSByte
    pkt->subcmd_21_23_04.reg1_val  = new_exposure & 0xFF;
    pkt->subcmd_21_23_04.reg2_addr = 0x3101; // R: 0x0131 - Set Exposure time MSByte
    pkt->subcmd_21_23_04.reg2_val  = (new_exposure & 0xFF00) >> 8;
    pkt->subcmd_21_23_04.reg3_addr = 0x0700; // R: 0x0007 - Finalize config - Without this, the register changes do not have any effect.
    pkt->subcmd_21_23_04.reg3_val  = 0x01;

    buf[48] = mcu_crc8_calc(buf + 12, 36);
    res = hid_write(handle, buf, sizeof(buf));

    return res;
}

int get_raw_ir_image(u8 show_status) {
    std::stringstream ir_status;

    int elapsed_time = 0;
    int elapsed_time2 = 0;
    System::Diagnostics::Stopwatch^ sw = System::Diagnostics::Stopwatch::StartNew();

    u8 buf[49];
    u8 buf_reply[0x170];
    u8 *buf_image = new u8[19 * 4096]; // 8bpp greyscale image.
	uint16_t bad_signal = 0;
    int error_reading = 0;
    float noise_level = 0.0f;
    int avg_intensity_percent = 0.0f;
    int previous_frag_no = 0;
    int got_frag_no = 0;
    int missed_packet_no = 0;
    bool missed_packet = false;
    int initialization = 2;
    int max_pixels = ((ir_max_frag_no < 218 ? ir_max_frag_no : 217) + 1) * 300;
    int white_pixels_percent = 0;

    memset(buf_image, 0, sizeof(buf_image));

    memset(buf, 0, sizeof(buf));
    memset(buf_reply, 0, sizeof(buf_reply));
    auto hdr = (brcm_hdr *)buf;
    auto pkt = (brcm_cmd_01 *)(hdr + 1);
    hdr->cmd = 0x11;
    pkt->subcmd = 0x03;
    buf[48] = 0xFF;

    // First ack
    hdr->timer = timming_byte & 0xF;
    timming_byte++;
    buf[14] = 0x0;
    buf[47] = mcu_crc8_calc(buf + 11, 36);
    hid_write(handle, buf, sizeof(buf));

    // IR Read/ACK loop for fragmented data packets. 
    // It also avoids requesting missed data fragments, we just skip it to not complicate things.
    while (enable_IRVideoPhoto || initialization) {
        memset(buf_reply, 0, sizeof(buf_reply));
        hid_read_timeout(handle, buf_reply, sizeof(buf_reply), 200);

        //Check if new packet
        if (buf_reply[0] == 0x31 && buf_reply[49] == 0x03) {
            got_frag_no = buf_reply[52];
            if (got_frag_no == (previous_frag_no + 1) % (ir_max_frag_no + 1)) {
                
                previous_frag_no = got_frag_no;

                // ACK for fragment
                hdr->timer = timming_byte & 0xF;
                timming_byte++;
                buf[14] = previous_frag_no;
                buf[47] = mcu_crc8_calc(buf + 11, 36);
                hid_write(handle, buf, sizeof(buf));

                memcpy(buf_image + (300 * got_frag_no), buf_reply + 59, 300);

                // Auto exposure.
                // TODO: Fix placement, so it doesn't drop next fragment.
                if (enable_IRAutoExposure && initialization < 2 && got_frag_no == 0){
                    white_pixels_percent = (int)((*(u16*)&buf_reply[55] * 100) / max_pixels);
                    ir_sensor_auto_exposure(white_pixels_percent);
                }

                // Status percentage
                ir_status.str("");
                ir_status.clear();
                if (initialization < 2) {
                    if (show_status == 2)
                        ir_status << "Status: Streaming.. ";
                    else
                        ir_status << "Status: Receiving.. ";
                }
                else
                    ir_status << "Status: Initializing.. ";
                ir_status << std::setfill(' ') << std::setw(3);
                ir_status << std::fixed << std::setprecision(0) << (float)got_frag_no / (float)(ir_max_frag_no + 1) * 100.0f;
                ir_status << "% - ";

                //debug
               // printf("%02X Frag: Copy\n", got_frag_no);

                FormJoy::myform1->lbl_IRStatus->Text = gcnew String(ir_status.str().c_str()) + (sw->ElapsedMilliseconds - elapsed_time).ToString() + "ms";
                elapsed_time = sw->ElapsedMilliseconds;

                // Check if final fragment. Draw the frame.
                if (got_frag_no == ir_max_frag_no) {
                    // Update Viewport
                    elapsed_time2 = sw->ElapsedMilliseconds - elapsed_time2;
                    FormJoy::myform1->setIRPictureWindow(buf_image, true);

                    //debug
                    //printf("%02X Frag: Draw -------\n", got_frag_no);

                    // Stats/IR header parsing
                    // buf_reply[53]: Average Intensity. 0-255 scale.
                    // buf_reply[54]: Unknown. Shows up only when EXFilter is enabled.
                    // *(u16*)&buf_reply[55]: White pixels (pixels with 255 value). Max 65535. Uint16 constraints, even though max is 76800.
                    // *(u16*)&buf_reply[57]: Pixels with ambient noise from external light sources (sun, lighter, IR remotes, etc). Cleaned by External Light Filter.
                    noise_level = (float)(*(u16*)&buf_reply[57]) / ((float)(*(u16*)&buf_reply[55]) + 1.0f);
                    white_pixels_percent = (int)((*(u16*)&buf_reply[55] * 100) / max_pixels);
                    avg_intensity_percent = (int)((buf_reply[53] * 100) / 255);
                    FormJoy::myform1->lbl_IRHelp->Text = String::Format("Amb Noise: {0:f2},  Int: {1:D}%,  FPS: {2:D} ({3:D}ms)\nEXFilter: {4:D},  White Px: {5:D}%,  EXF Int: {6:D}",
                        noise_level, avg_intensity_percent, (int)(1000 / elapsed_time2), elapsed_time2, *(u16*)&buf_reply[57], white_pixels_percent, buf_reply[54]);

                    elapsed_time2 = sw->ElapsedMilliseconds;

                    if (initialization)
                        initialization--;
                }
                Application::DoEvents();
            }
            // Repeat/Missed fragment
            else if (got_frag_no  || previous_frag_no) {
                // Check if repeat ACK should be send. Avoid writing to image buffer.
                if (got_frag_no == previous_frag_no) {
                    //debug
                    //printf("%02X Frag: Repeat\n", got_frag_no);

                    // ACK for fragment
                    hdr->timer = timming_byte & 0xF;
                    timming_byte++;
                    buf[14] = got_frag_no;
                    buf[47] = mcu_crc8_calc(buf + 11, 36);
                    hid_write(handle, buf, sizeof(buf));

                    missed_packet = false;
                }
                // Check if missed fragment and request it.
                else if(missed_packet_no != got_frag_no && !missed_packet) {
                    if (ir_max_frag_no != 0x03) {
                        //debug
                        //printf("%02X Frag: Missed %02X, Prev: %02X, PrevM: %02X\n", got_frag_no, previous_frag_no + 1, previous_frag_no, missed_packet_no);

                        // Missed packet
                        hdr->timer = timming_byte & 0xF;
                        timming_byte++;
                        //Request for missed packet. You send what the next fragment number will be, instead of the actual missed packet.
                        buf[12] = 0x1;
                        buf[13] = previous_frag_no + 1;
                        buf[14] = 0;
                        buf[47] = mcu_crc8_calc(buf + 11, 36);
                        hid_write(handle, buf, sizeof(buf));

                        buf[12] = 0x00;
                        buf[13] = 0x00;

                        memcpy(buf_image + (300 * got_frag_no), buf_reply + 59, 300);

                        previous_frag_no = got_frag_no;
                        missed_packet_no = got_frag_no - 1;
                        missed_packet = true;
                    }
                    // Check if missed fragment and res is 30x40. Don't request it.
                    else {
                        //debug
                        //printf("%02X Frag: Missed but res is 30x40\n", got_frag_no);

                        // ACK for fragment
                        hdr->timer = timming_byte & 0xF;
                        timming_byte++;
                        buf[14] = got_frag_no;
                        buf[47] = mcu_crc8_calc(buf + 11, 36);
                        hid_write(handle, buf, sizeof(buf));

                        memcpy(buf_image + (300 * got_frag_no), buf_reply + 59, 300);

                        previous_frag_no = got_frag_no;
                    }
                }
                // Got the requested missed fragments.
                else if (missed_packet_no == got_frag_no){
                    //debug
                    //printf("%02X Frag: Got missed %02X\n", got_frag_no, missed_packet_no);

                    // ACK for fragment
                    hdr->timer = timming_byte & 0xF;
                    timming_byte++;
                    buf[14] = got_frag_no;
                    buf[47] = mcu_crc8_calc(buf + 11, 36);
                    hid_write(handle, buf, sizeof(buf));

                    memcpy(buf_image + (300 * got_frag_no), buf_reply + 59, 300);

                    previous_frag_no = got_frag_no;
                    missed_packet = false;
                }
                // Repeat of fragment that is not max fragment.
                else {
                    //debug
                    //printf("%02X Frag: RepeatWoot M:%02X\n", got_frag_no, missed_packet_no);

                    // ACK for fragment
                    hdr->timer = timming_byte & 0xF;
                    timming_byte++;
                    buf[14] = got_frag_no;
                    buf[47] = mcu_crc8_calc(buf + 11, 36);
                    hid_write(handle, buf, sizeof(buf));
                }
                
                // Status percentage
                ir_status.str("");
                ir_status.clear();
                if (initialization < 2) {
                    if (show_status == 2)
                        ir_status << "Status: Streaming.. ";
                    else
                        ir_status << "Status: Receiving.. ";
                }
                else
                    ir_status << "Status: Initializing.. ";
                ir_status << std::setfill(' ') << std::setw(3);
                ir_status << std::fixed << std::setprecision(0) << (float)got_frag_no / (float)(ir_max_frag_no + 1) * 100.0f;
                ir_status << "% - ";

                FormJoy::myform1->lbl_IRStatus->Text = gcnew String(ir_status.str().c_str()) + (sw->ElapsedMilliseconds - elapsed_time).ToString() + "ms";
                elapsed_time = sw->ElapsedMilliseconds;
                Application::DoEvents();
            }
            
            // Streaming start
            else {
                // ACK for fragment
                hdr->timer = timming_byte & 0xF;
                timming_byte++;
                buf[14] = got_frag_no;
                buf[47] = mcu_crc8_calc(buf + 11, 36);
                hid_write(handle, buf, sizeof(buf));

                memcpy(buf_image + (300 * got_frag_no), buf_reply + 59, 300);

                //debug
                //printf("%02X Frag: 0 %02X\n", buf_reply[52], previous_frag_no);

                FormJoy::myform1->lbl_IRStatus->Text = (sw->ElapsedMilliseconds - elapsed_time).ToString() + "ms";
                elapsed_time = sw->ElapsedMilliseconds;
                Application::DoEvents();

                previous_frag_no = 0;
            }

        }
        // Empty IR report. Send Ack again. Otherwise, it fallbacks to high latency mode (30ms per data fragment)
        else if (buf_reply[0] == 0x31) {
            // ACK for fragment
            hdr->timer = timming_byte & 0xF;
            timming_byte++;

            // Send ACK again or request missed frag
            if (buf_reply[49] == 0xFF) {
                buf[14] = previous_frag_no;
            }
            else if (buf_reply[49] == 0x00) {
                buf[12] = 0x1;
                buf[13] = previous_frag_no + 1;
                buf[14] = 0;
               // printf("%02X Mode: Missed next packet %02X\n", buf_reply[49], previous_frag_no + 1);
            }

            buf[47] = mcu_crc8_calc(buf + 11, 36);
            hid_write(handle, buf, sizeof(buf));

            buf[12] = 0x00;
            buf[13] = 0x00;
        }
    }
    
    delete[] buf_image;

    return 0;
}


int ir_sensor(ir_image_config &ir_cfg) {
    int res;
    u8 buf[0x170];
    static int output_buffer_length = 49;
    int error_reading = 0;
    int res_get = 0;
    // Set input report to x31
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x03;
        pkt->subcmd_arg.arg1 = 0x31;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (*(u16*)&buf[0xD] == 0x0380)
                goto step1;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 1;
            goto step10;
        }
    }

step1:
    // Enable MCU
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x22;
        pkt->subcmd_arg.arg1 = 0x1;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (*(u16*)&buf[0xD] == 0x2280)
                goto step2;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 2;
            goto step10;
        }
    }

step2:
    // Request MCU mode status
    error_reading = 0;
    while (1) { // Not necessary, but we keep to make sure the MCU is ready.
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x01;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x31) {
                //if (buf[49] == 0x01 && buf[56] == 0x06) // MCU state is Initializing
                // *(u16*)buf[52]LE x04 in lower than 3.89fw, x05 in 3.89
                // *(u16*)buf[54]LE x12 in lower than 3.89fw, x18 in 3.89
                // buf[56]: mcu mode state
                if (buf[49] == 0x01 && buf[56] == 0x01) // MCU state is Standby
                    goto step3;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 3;
            goto step10;
        }
    }

step3:
    // Set MCU mode
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x01;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x21;
        
        pkt->subcmd_21_21.mcu_cmd    = 0x21; // Set MCU mode cmd
        pkt->subcmd_21_21.mcu_subcmd = 0x00; // Set MCU mode cmd
        pkt->subcmd_21_21.mcu_mode   = 0x05; // MCU mode - 1: Standby, 4: NFC, 5: IR, 6: Initializing/FW Update?

        buf[48] = mcu_crc8_calc(buf + 12, 36);
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x21) {
                // *(u16*)buf[18]LE x04 in lower than 3.89fw, x05 in 3.89
                // *(u16*)buf[20]LE x12 in lower than 3.89fw, x18 in 3.89
                // buf[56]: mcu mode state
                if (buf[15] == 0x01 && *(u32*)&buf[22] == 0x01) // Mcu mode is Standby
                    goto step4;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 4;
            goto step10;
        }
    }

step4:
    // Request MCU mode status
    error_reading = 0;
    while (1) { // Not necessary, but we keep to make sure the MCU mode changed.
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x01;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x31) {
                // *(u16*)buf[52]LE x04 in lower than 3.89fw, x05 in 3.89
                // *(u16*)buf[54]LE x12 in lower than 3.89fw, x18 in 3.89
                if (buf[49] == 0x01 && buf[56] == 0x05) // Mcu mode is IR
                    goto step5;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 5;
            goto step10;
        }
    }

step5:
    // Set IR mode and number of packets for each data blob. Blob size is packets * 300 bytes.
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x01;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;

        pkt->subcmd = 0x21;
        pkt->subcmd_21_23_01.mcu_cmd     = 0x23;
        pkt->subcmd_21_23_01.mcu_subcmd  = 0x01; // Set IR mode cmd
        pkt->subcmd_21_23_01.mcu_ir_mode = 0x07; // IR mode - 2: No mode/Disable?, 3: Moment, 4: Dpd (Wii-style pointing), 6: Clustering,
                                                 // 7: Image transfer, 8-10: Hand analysis (Silhouette, Image, Silhouette/Image), 0,1/5/10+: Unknown
        pkt->subcmd_21_23_01.no_of_frags = ir_max_frag_no; // Set number of packets to output per buffer
        pkt->subcmd_21_23_01.mcu_major_v = 0x0500; // Set required IR MCU FW v5.18. Major 0x0005.
        pkt->subcmd_21_23_01.mcu_minor_v = 0x1800; // Set required IR MCU FW v5.18. Minor 0x0018.

        buf[48] = mcu_crc8_calc(buf + 12, 36);
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x21) {
                // Mode set Ack
                if (buf[15] == 0x0b)
                    goto step6;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 6;
            goto step10;
        }
    }

step6:
    // Request IR mode status
    error_reading = 0;
    while (0) { // Not necessary
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;

        pkt->subcmd = 0x03;
        pkt->subcmd_arg.arg1 = 0x02;

        buf[47] = mcu_crc8_calc(buf + 11, 36);
        buf[48] = 0xFF;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x31) {
                // mode set to 7: Image transfer
                if (buf[49] == 0x13 && *(u16*)&buf[50] == 0x0700)
                    goto step7;
            }
            retries++;
            if (retries > 4 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 7;
            goto step10;
        }
    }

step7:
    // Write to registers for the selected IR mode
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x01;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x21;

        pkt->subcmd_21_23_04.mcu_cmd    = 0x23; // Write register cmd
        pkt->subcmd_21_23_04.mcu_subcmd = 0x04; // Write register to IR mode subcmd
        pkt->subcmd_21_23_04.no_of_reg  = 0x09; // Number of registers to write. Max 9.      

        pkt->subcmd_21_23_04.reg1_addr  = 0x2e00; // R: 0x002e - Set Resolution based on sensor binning and skipping
        pkt->subcmd_21_23_04.reg1_val   = ir_cfg.ir_res_reg;
        pkt->subcmd_21_23_04.reg2_addr  = 0x3001; // R: 0x0130 - Set Exposure time LSByte - (31200 * us /1000) & 0xFF - Max: 600us, Max encoded: 0x4920.
        pkt->subcmd_21_23_04.reg2_val   = ir_cfg.ir_exposure & 0xFF;
        pkt->subcmd_21_23_04.reg3_addr  = 0x3101; // R: 0x0131 - Set Exposure time MSByte - ((31200 * us /1000) & 0xFF00) >> 8
        pkt->subcmd_21_23_04.reg3_val   = (ir_cfg.ir_exposure & 0xFF00) >> 8;
        pkt->subcmd_21_23_04.reg4_addr  = 0x3201; // R: 0x0132 - Enable Max exposure Time - 0: Manual exposure, 1: Max exposure
        pkt->subcmd_21_23_04.reg4_val   = 0x00;
        pkt->subcmd_21_23_04.reg5_addr  = 0x1000; // R: 0x0010 - Set IR Leds groups state - Only 3 LSB usable
        pkt->subcmd_21_23_04.reg5_val   = ir_cfg.ir_leds;
        pkt->subcmd_21_23_04.reg6_addr  = 0x2e01; // R: 0x012e - Set digital gain LSB 4 bits of the value - 0-0xff
        pkt->subcmd_21_23_04.reg6_val   = (ir_cfg.ir_digital_gain & 0xF) << 4;
        pkt->subcmd_21_23_04.reg7_addr  = 0x2f01; // R: 0x012f - Set digital gain MSB 4 bits of the value - 0-0x7
        pkt->subcmd_21_23_04.reg7_val   = (ir_cfg.ir_digital_gain & 0xF0) >> 4;
        pkt->subcmd_21_23_04.reg8_addr  = 0x0e00; // R: 0x00e0 - External light filter - LS o bit0: Off/On, bit1: 0x/1x, bit2: ??, bit4,5: ??.
        pkt->subcmd_21_23_04.reg8_val   = ir_cfg.ir_ex_light_filter;
        pkt->subcmd_21_23_04.reg9_addr  = 0x4301; // R: 0x0143 - ExLF/White pixel stats threshold - 200: Default
        pkt->subcmd_21_23_04.reg9_val   = 0xc8;

        buf[48] = mcu_crc8_calc(buf + 12, 36);
        res = hid_write(handle, buf, output_buffer_length);

        // Request IR mode status, before waiting for the x21 ack
        memset(buf, 0, sizeof(buf));
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x03;
        pkt->subcmd_arg.arg1 = 0x02;
        buf[47] = mcu_crc8_calc(buf + 11, 36);
        buf[48] = 0xFF;
        res = hid_write(handle, buf, output_buffer_length);

        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x21) {
                // Registers for mode 7: Image transfer set
                if (buf[15] == 0x13 && *(u16*)&buf[16] == 0x0700)
                    goto step8;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 8;
            goto step10;
        }
    }

step8:
    // Write to registers for the selected IR mode
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x01;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x21;

        pkt->subcmd_21_23_04.mcu_cmd    = 0x23; // Write register cmd
        pkt->subcmd_21_23_04.mcu_subcmd = 0x04; // Write register to IR mode subcmd
        pkt->subcmd_21_23_04.no_of_reg  = 0x08; // Number of registers to write. Max 9.      

        pkt->subcmd_21_23_04.reg1_addr  = 0x1100; // R: 0x0011 - Leds 1/2 Intensity - Max 0x0F.
        pkt->subcmd_21_23_04.reg1_val   = (ir_cfg.ir_leds_intensity >> 8) & 0xFF;
        pkt->subcmd_21_23_04.reg2_addr  = 0x1200; // R: 0x0012 - Leds 3/4 Intensity - Max 0x10.
        pkt->subcmd_21_23_04.reg2_val   = ir_cfg.ir_leds_intensity & 0xFF;
        pkt->subcmd_21_23_04.reg3_addr  = 0x2d00; // R: 0x002d - Flip image - 0: Normal, 1: Vertically, 2: Horizontally, 3: Both 
        pkt->subcmd_21_23_04.reg3_val   = ir_cfg.ir_flip;
        pkt->subcmd_21_23_04.reg4_addr  = 0x6701; // R: 0x0167 - Enable De-noise smoothing algorithms - 0: Disable, 1: Enable.
        pkt->subcmd_21_23_04.reg4_val   = (ir_cfg.ir_denoise >> 16) & 0xFF;
        pkt->subcmd_21_23_04.reg5_addr  = 0x6801; // R: 0x0168 - Edge smoothing threshold - Max 0xFF, Default 0x23
        pkt->subcmd_21_23_04.reg5_val   = (ir_cfg.ir_denoise >> 8) & 0xFF;
        pkt->subcmd_21_23_04.reg6_addr  = 0x6901; // R: 0x0169 - Color Interpolation threshold - Max 0xFF, Default 0x44
        pkt->subcmd_21_23_04.reg6_val   = ir_cfg.ir_denoise & 0xFF;
        pkt->subcmd_21_23_04.reg7_addr  = 0x0400; // R: 0x0004 - LSB Buffer Update Time - Default 0x32
        if (ir_cfg.ir_res_reg == 0x69)
            pkt->subcmd_21_23_04.reg7_val = 0x2d; // A value of <= 0x2d is fast enough for 30 x 40, so the first fragment has the updated frame.  
        else
            pkt->subcmd_21_23_04.reg7_val = 0x32; // All the other resolutions the default is enough. Otherwise a lower value can break hand analysis.
        pkt->subcmd_21_23_04.reg8_addr  = 0x0700; // R: 0x0007 - Finalize config - Without this, the register changes do not have any effect.
        pkt->subcmd_21_23_04.reg8_val   = 0x01;

        buf[48] = mcu_crc8_calc(buf + 12, 36);
        res = hid_write(handle, buf, output_buffer_length);

        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x21) {
                // Registers for mode 7: Image transfer set
                if (buf[15] == 0x13 && *(u16*)&buf[16] == 0x0700)
                    goto step9;
                // If the Joy-Con gets to reply to the previous x11 - x03 02 cmd before sending the above,
                // it will reply with the following if we do not send x11 - x03 02 again:
                else if (buf[15] == 0x23) // Got mcu mode config write.
                    goto step9;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 9;
            goto step10;
        }
    }

step9:
    // Stream or Capture images from NIR Camera
    if (enable_IRVideoPhoto)
        res_get = get_raw_ir_image(2);
    else
        res_get = get_raw_ir_image(1);

    //////
    // TODO: Should we send subcmd x21 with 'x230102' to disable IR mode before disabling MCU?
step10:
    // Disable MCU
    memset(buf, 0, sizeof(buf));
    auto hdr = (brcm_hdr *)buf;
    auto pkt = (brcm_cmd_01 *)(hdr + 1);
    hdr->cmd = 1;
    hdr->timer = timming_byte & 0xF;
    timming_byte++;
    pkt->subcmd = 0x22;
    pkt->subcmd_arg.arg1 = 0x00;
    res = hid_write(handle, buf, output_buffer_length);
    res = hid_read_timeout(handle, buf, sizeof(buf), 64);  


    // Set input report back to x3f
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x03;
        pkt->subcmd_arg.arg1 = 0x3f;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (*(u16*)&buf[0xD] == 0x0380)
                goto stepf;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            goto stepf;
        }
    }

stepf:
    return res_get;
}

int get_ir_registers(int start_reg, int reg_group) {
    int res;
    u8 buf[0x170];
    static int output_buffer_length = 49;
    int error_reading = 0;
    int res_get = 0;

    // Get the IR registers
    error_reading = 0;
    int pos_ir_registers = start_reg;
    while (1) {
    repeat_send:
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        memset(buf, 0, sizeof(buf));
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x03;
        pkt->subcmd_arg.arg1 = 0x03;

        buf[12] = 0x1; // seems to be always 0x01

        buf[13] = pos_ir_registers; // 0-4 registers page/group
        buf[14] = 0x00; // offset. this plus the number of registers, must be less than x7f
        buf[15] = 0x7f; // Number of registers to show + 1

        buf[47] = mcu_crc8_calc(buf + 11, 36);

        res = hid_write(handle, buf, output_buffer_length);

        int tries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[49] == 0x1b && buf[51] == pos_ir_registers && buf[52] == 0x00) {
                error_reading = 0;
                printf("--->%02X, %02X : %02X:\n", buf[51], buf[52], buf[53]);
                for (int i = 0; i <= buf[52] + buf[53]; i++)
                    if ((i & 0xF) == 0xF)
                        printf("%02X | ", buf[54 + i]);
                    else
                        printf("%02X ", buf[54 + i]);
                printf("\n");
                break;
            }
            tries++;
            if (tries > 8) {
                error_reading++;
                if (error_reading > 5) {
                    return 1;
                }
                goto repeat_send;
            }

        }
        pos_ir_registers++;
        if (pos_ir_registers > reg_group) {
            break;
        }
        
    }
    printf("\n");

    return 0;
}

int ir_sensor_config_live(ir_image_config &ir_cfg) {
    int res;
    u8 buf[49];

    memset(buf, 0, sizeof(buf));
    auto hdr = (brcm_hdr *)buf;
    auto pkt = (brcm_cmd_01 *)(hdr + 1);
    hdr->cmd = 0x01;
    hdr->timer = timming_byte & 0xF;
    timming_byte++;
    pkt->subcmd = 0x21;

    pkt->subcmd_21_23_04.mcu_cmd    = 0x23; // Write register cmd
    pkt->subcmd_21_23_04.mcu_subcmd = 0x04; // Write register to IR mode subcmd
    pkt->subcmd_21_23_04.no_of_reg  = 0x09; // Number of registers to write. Max 9.

    pkt->subcmd_21_23_04.reg1_addr = 0x3001; // R: 0x0130 - Set Exposure time LSByte
    pkt->subcmd_21_23_04.reg1_val  = ir_cfg.ir_exposure & 0xFF;
    pkt->subcmd_21_23_04.reg2_addr = 0x3101; // R: 0x0131 - Set Exposure time MSByte
    pkt->subcmd_21_23_04.reg2_val  = (ir_cfg.ir_exposure & 0xFF00) >> 8;
    pkt->subcmd_21_23_04.reg3_addr = 0x1000; // R: 0x0010 - Set IR Leds groups state
    pkt->subcmd_21_23_04.reg3_val  = ir_cfg.ir_leds;
    pkt->subcmd_21_23_04.reg4_addr = 0x2e01; // R: 0x012e - Set digital gain LSB 4 bits
    pkt->subcmd_21_23_04.reg4_val  = (ir_cfg.ir_digital_gain & 0xF) << 4;
    pkt->subcmd_21_23_04.reg5_addr = 0x2f01; // R: 0x012f - Set digital gain MSB 4 bits
    pkt->subcmd_21_23_04.reg5_val  = (ir_cfg.ir_digital_gain & 0xF0) >> 4;
    pkt->subcmd_21_23_04.reg6_addr = 0x0e00; // R: 0x00e0 - External light filter
    pkt->subcmd_21_23_04.reg6_val  = ir_cfg.ir_ex_light_filter;
    pkt->subcmd_21_23_04.reg7_addr = (ir_cfg.ir_custom_register & 0xFF) << 8 | (ir_cfg.ir_custom_register >> 8) & 0xFF;
    pkt->subcmd_21_23_04.reg7_val  = (ir_cfg.ir_custom_register >> 16) & 0xFF;
    pkt->subcmd_21_23_04.reg8_addr = 0x1100; // R: 0x0011 - Leds 1/2 Intensity - Max 0x0F.
    pkt->subcmd_21_23_04.reg8_val  = (ir_cfg.ir_leds_intensity >> 8) & 0xFF;
    pkt->subcmd_21_23_04.reg9_addr = 0x1200; // R: 0x0012 - Leds 3/4 Intensity - Max 0x10.
    pkt->subcmd_21_23_04.reg9_val  = ir_cfg.ir_leds_intensity & 0xFF;

    buf[48] = mcu_crc8_calc(buf + 12, 36);
    res = hid_write(handle, buf, sizeof(buf));

    // Important. Otherwise we gonna have a dropped packet.
    Sleep(15);

    pkt->subcmd_21_23_04.no_of_reg = 0x06; // Number of registers to write. Max 9.

    pkt->subcmd_21_23_04.reg1_addr = 0x2d00; // R: 0x002d - Flip image - 0: Normal, 1: Vertically, 2: Horizontally, 3: Both 
    pkt->subcmd_21_23_04.reg1_val  = ir_cfg.ir_flip;
    pkt->subcmd_21_23_04.reg2_addr = 0x6701; // R: 0x0167 - Enable De-noise smoothing algorithms - 0: Disable, 1: Enable.
    pkt->subcmd_21_23_04.reg2_val  = (ir_cfg.ir_denoise >> 16) & 0xFF;
    pkt->subcmd_21_23_04.reg3_addr = 0x6801; // R: 0x0168 - Edge smoothing threshold - Max 0xFF, Default 0x23
    pkt->subcmd_21_23_04.reg3_val  = (ir_cfg.ir_denoise >> 8) & 0xFF;
    pkt->subcmd_21_23_04.reg4_addr = 0x6901; // R: 0x0169 - Color Interpolation threshold - Max 0xFF, Default 0x44
    pkt->subcmd_21_23_04.reg4_val  = ir_cfg.ir_denoise & 0xFF;
    pkt->subcmd_21_23_04.reg5_addr = 0x0400; // R: 0x0004 - LSB Buffer Update Time - Default 0x32
    if (ir_cfg.ir_res_reg == 0x69)
        pkt->subcmd_21_23_04.reg5_val = 0x2d; // A value of <= 0x2d is fast enough for 30 x 40, so the first fragment has the updated frame.  
    else
        pkt->subcmd_21_23_04.reg5_val = 0x32; // All the other resolutions the default is enough. Otherwise a lower value can break hand analysis.
    pkt->subcmd_21_23_04.reg6_addr = 0x0700; // R: 0x0007 - Finalize config - Without this, the register changes do not have any effect.
    pkt->subcmd_21_23_04.reg6_val  = 0x01;

    buf[48] = mcu_crc8_calc(buf + 12, 36);
    res = hid_write(handle, buf, sizeof(buf));

    // get_ir_registers(0,4); // Get all register pages
    // get_ir_registers((ir_cfg.ir_custom_register >> 8) & 0xFF, (ir_cfg.ir_custom_register >> 8) & 0xFF); // Get all registers based on changed register's page

    return res;
}


int nfc_tag_info() {
    /////////////////////////////////////////////////////
    // Kudos to Eric Betts (https://github.com/bettse) //
    // for nfc comm starters                           //
    /////////////////////////////////////////////////////
    int res;
    u8 buf[0x170];
    u8 buf2[0x170];
    static int output_buffer_length = 49;
    int error_reading = 0;
    int res_get = 0;
    u8  tag_uid_buf[10];
    u8  tag_uid_size = 0;
    u8  ntag_buffer[924];
    u16 ntag_buffer_pos = 0;
    u8  ntag_pages = 0; // Max 231
    u8  tag_type = 0;
    u16 payload_size = 0;
    bool ntag_init_done = false;

    memset(tag_uid_buf, 0, sizeof(tag_uid_buf));
    memset(ntag_buffer, 0, sizeof(ntag_buffer));

    // Set input report to x31
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x03;
        pkt->subcmd_arg.arg1 = 0x31;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (*(u16*)&buf[0xD] == 0x0380)
                goto step1;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 1;
            goto step9;
        }
    }

step1:
    // Enable MCU
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x22;
        pkt->subcmd_arg.arg1 = 0x1;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (*(u16*)&buf[0xD] == 0x2280)
                goto step2;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 2;
            goto step9;
        }
    }

step2:
    // Request MCU mode status
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x01;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x31) {
                //if (buf[49] == 0x01 && buf[56] == 0x06) // MCU state is Initializing
                // *(u16*)buf[52]LE x04 in lower than 3.89fw, x05 in 3.89
                // *(u16*)buf[54]LE x12 in lower than 3.89fw, x18 in 3.89
                // buf[56]: mcu mode state
                if (buf[49] == 0x01 && buf[56] == 0x01) // Mcu mode is Standby
                    goto step3;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 3;
            goto step9;
        }
    }

step3:
    // Set MCU mode
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x01;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x21;

        pkt->subcmd_21_21.mcu_cmd = 0x21; // Set MCU mode cmd
        pkt->subcmd_21_21.mcu_subcmd = 0x00; // Set MCU mode cmd
        pkt->subcmd_21_21.mcu_mode = 0x04; // MCU mode - 1: Standby, 4: NFC, 5: IR, 6: Initializing/FW Update?

        buf[48] = mcu_crc8_calc(buf + 12, 36);
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x21) {
                // *(u16*)buf[18]LE x04 in lower than 3.89fw, x05 in 3.89
                // *(u16*)buf[20]LE x12 in lower than 3.89fw, x18 in 3.89
                if (buf[15] == 0x01 && buf[22] == 0x01) // Mcu mode is standby
                    goto step4;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 4;
            goto step9;
        }
    }

step4:
    // Request MCU mode status
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x01;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x31) {
                // *(u16*)buf[52]LE x04 in lower than 3.89fw, x05 in 3.89
                // *(u16*)buf[54]LE x12 in lower than 3.89fw, x18 in 3.89
                if (buf[49] == 0x01 && buf[56] == 0x04) // Mcu mode is NFC
                    goto step5;
            }
            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            res_get = 5;
            goto step9;
        }
    }

step5:
    // Request NFC mode status
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;

        pkt->subcmd = 0x02;
        pkt->subcmd_arg.arg1 = 0x04; // 0: Cancel all, 4: StartWaitingReceive
        pkt->subcmd_arg.arg2 = 0x00; // Count of the currecnt packet if the cmd is a series of packets.
        buf[13] = 0x00;
        buf[14] = 0x08; // 8: Last cmd packet, 0: More cmd packet should be  expected
        buf[15] = 0x00; // Length of data after cmd header

        buf[47] = mcu_crc8_calc(buf + 11, 36); //Without the last byte
        res = hid_write(handle, buf, output_buffer_length - 1);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x31) {
                if (buf[49] == 0x2a && *(u16*)&buf[50] == 0x0500 && buf[55] == 0x31 && buf[56] == 0x0b)// buf[56] == 0x0b: Initializing/Busy
                    break;
                if (buf[49] == 0x2a && *(u16*)&buf[50] == 0x0500 && buf[55] == 0x31 && buf[56] == 0x00) // buf[56] == 0x00: Awaiting cmd
                    goto step6;
            }
            retries++;
            if (retries > 4 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 9) {
            res_get = 6;
            goto step9;
        }
    }

step6:
    // Request NFC mode status
    error_reading = 0;
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;

        pkt->subcmd = 0x02;
        pkt->subcmd_arg.arg1 = 0x01; // 1: Start polling, 2: Stop polling, 
        pkt->subcmd_arg.arg2 = 0x00; // Count of the currecnt packet if the cmd is a series of packets.
        buf[13] = 0x00;
        buf[14] = 0x08; // 8: Last cmd packet, 0: More cmd packet should be expected
        buf[15] = 0x05; // Length of data after cmd header
        buf[16] = 0x01; // 1: Enable Mifare support
        buf[17] = 0x00; // Unknown.
        buf[18] = 0x00; // Unknown.
        buf[19] = 0x2c; // Unknown. Some values work (0x07) other don't.
        buf[20] = 0x01; // Unknown. This is not needed but Switch sends it.

        buf[47] = mcu_crc8_calc(buf + 11, 36); //Without the last byte
        res = hid_write(handle, buf, output_buffer_length - 1);
        int retries = 0;
        while (1) {
            if (!enable_NFCScanning)
                goto step7;
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (buf[0] == 0x31) {
                // buf[49] == 0x2a: NFC MCU input report
                // buf[50] shows when there's error?
                // buf[51] == 0x05: NFC
                // buf[54] always 9?
                // buf[55] always x31?
                // buf[56]: MCU/NFC state
                // buf[62]: nfc tag IC
                // buf[63]: nfc tag Type
                // buf[64]: size of following data and it's the last NFC header byte
                if (buf[49] == 0x2a && *(u16*)&buf[50] == 0x0500 && buf[56] == 0x09) { // buf[56] == 0x09: Tag detected
                    tag_uid_size = buf[64];
                    FormJoy::myform1->txtBox_nfcUid->Text = "UID:  ";
                    for (int i = 0; i < tag_uid_size; i++) {
                        if (i < tag_uid_size - 1) {
                            tag_type = buf[62]; // Save tag type
                            tag_uid_buf[i] = buf[65 + i]; // Save UID
                            FormJoy::myform1->txtBox_nfcUid->Text += String::Format("{0:X2}:", buf[65 + i]);
                        }
                        else {
                            FormJoy::myform1->txtBox_nfcUid->Text += String::Format("{0:X2}", buf[65 + i]);
                        }
                    }
                    FormJoy::myform1->txtBox_nfcUid->Text += String::Format("\r\nType: {0:s}", buf[62] == 0x2 ? "NTAG" : "MIFARE");
                    Application::DoEvents();
                    goto step7;
                }
                else if (buf[49] == 0x2a)
                    break;
            }
            retries++;
            if (retries > 4 || res == 0) {
                Application::DoEvents();
                break;
            }
        }
        error_reading++;
        if (error_reading > 100) {
            res_get = 7;
            if (ntag_init_done)
                FormJoy::myform1->txtBox_NFCTag->Text = String::Format("Tag lost!");
            else
                FormJoy::myform1->txtBox_NFCTag->Text = String::Format("No Tag detected!");
            goto step9;
        }
    }

step7:
    // Read NTAG contents
    error_reading = 0;
    while (1) {
        memset(buf2, 0, sizeof(buf2));
        auto hdr = (brcm_hdr *)buf2;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 0x11;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;

        pkt->subcmd = 0x02;
        pkt->subcmd_arg.arg1 = 0x06; // 6: Read Ntag data, 0xf: Read mifare data
        buf2[12] = 0x00;
        buf2[13] = 0x00;
        buf2[14] = 0x08;
        buf2[15] = 0x13; // Length of data after cmd header

        buf2[16] = 0xd0; // Unknown
        buf2[17] = 0x07; // Unknown or UID lentgh?
        buf2[18] = 0x00; // Only for Mifare cmds or should have a UID?

        buf2[19] = 0x00; //should have a UID?
        buf2[20] = 0x00; //should have a UID?
        buf2[21] = 0x00; //should have a UID?
        buf2[22] = 0x00; //should have a UID?
        buf2[23] = 0x00; //should have a UID?
        buf2[24] = 0x00; //should have a UID?

        buf2[25] = 0x00; // 1: Ntag215 only. 0: All tags, otherwise error x48 (Invalid format error)

        // https://www.tagnfc.com/en/info/11-nfc-tags-specs

        // If the following is selected wrongly, error x3e (Read error)
        switch (ntag_pages) {
            case 0:
                buf2[26] = 0x01;
                break;
                // Ntag213
            case 45:
                // The following 7 bytes should be decided with max the current ntag pages and what we want to read.
                buf2[26] = 0x01; // How many blocks to read. Each block should be <= 60 pages (240 bytes)? Min/Max values are 1/4, otherwise error x40 (Argument error)

                buf2[27] = 0x00; // Block 1 starting page
                buf2[28] = 0x2C; // Block 1 ending page
                buf2[29] = 0x00; // Block 2 starting page
                buf2[30] = 0x00; // Block 2 ending page
                buf2[31] = 0x00; // Block 3 starting page
                buf2[32] = 0x00; // Block 3 ending page
                buf2[33] = 0x00; // Block 4 starting page
                buf2[34] = 0x00; // Block 4 ending page
                break;
                // Ntag215
            case 135:
                // The following 7 bytes should be decided with max the current ntag pages and what we want to read.
                buf2[26] = 0x03; // How many page ranges to read. Each range should be <= 60 pages (240 bytes)? Max value is 4.

                buf2[27] = 0x00; // Block 1 starting page
                buf2[28] = 0x3b; // Block 1 ending page
                buf2[29] = 0x3c; // Block 2 starting page
                buf2[30] = 0x77; // Block 2 ending page
                buf2[31] = 0x78; // Block 3 starting page
                buf2[32] = 0x86; // Block 3 ending page
                buf2[33] = 0x00; // Block 4 starting page
                buf2[34] = 0x00; // Block 4 ending page
                break;
            case 231:
                // The following 7 bytes should be decided with max the current ntag pages and what we want to read.
                buf2[26] = 0x04; // How many page ranges to read. Each range should be <= 60 pages (240 bytes)? Max value is 4.

                buf2[27] = 0x00; // Block 1 starting page
                buf2[28] = 0x3b; // Block 1 ending page
                buf2[29] = 0x3c; // Block 2 starting page
                buf2[30] = 0x77; // Block 2 ending page
                buf2[31] = 0x78; // Block 3 starting page
                buf2[32] = 0xB3; // Block 3 ending page
                buf2[33] = 0xB4; // Block 4 starting page
                buf2[34] = 0xE6; // Block 4 ending page
                break;
            default:
                break;
        }

        buf2[47] = mcu_crc8_calc(buf2 + 11, 36);

        res = hid_write(handle, buf2, output_buffer_length - 1);

        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf2, sizeof(buf2), 64);
            if (buf2[0] == 0x31) {
                if ((buf2[49] == 0x3a || buf2[49] == 0x2a) && buf2[56] == 0x07) {
                    FormJoy::myform1->txtBox_NFCTag->Text = String::Format("Error {0:X2}!", buf2[50]);
                    goto step9;///////////
                }
                else if (buf2[49] == 0x3a && buf2[51] == 0x07) {
                    if (ntag_init_done) {
                        payload_size = (buf2[54] << 8 | buf2[55]) & 0x7FF;
                        if (buf2[52] == 0x01) {
                            memcpy(ntag_buffer + ntag_buffer_pos, buf2 + 116, payload_size - 60);
                            ntag_buffer_pos += payload_size - 60;
                        }
                        else {
                            memcpy(ntag_buffer + ntag_buffer_pos, buf2 + 56, payload_size);
                        }
                    }
                    else if (buf2[52] == 0x01) {
                        if (tag_type == 2) {
                            switch (buf2[74]) {
                                case 0:
                                    ntag_pages = 135;
                                    break;
                                case 3:
                                    ntag_pages = 45;
                                    break;
                                case 4:
                                    ntag_pages = 231;
                                    break;
                                default:
                                    goto step9;///////////
                                    break;
                            }
                        }
                    }
                    break;
                }
                else if (buf2[49] == 0x2a && buf2[56] == 0x04) { // finished
                    if (ntag_init_done) {
                        FormJoy::myform1->show_ntag_contents(ntag_buffer, ntag_pages);
                        Application::DoEvents();
                        goto step9;///////////
                    }
                    ntag_init_done = true;

                    memset(buf, 0, sizeof(buf));
                    auto hdr = (brcm_hdr *)buf;
                    auto pkt = (brcm_cmd_01 *)(hdr + 1);
                    hdr->cmd = 0x11;
                    hdr->timer = timming_byte & 0xF;
                    timming_byte++;

                    pkt->subcmd = 0x02;
                    pkt->subcmd_arg.arg1 = 0x02; // 0: Cancel all, 4: StartWaitingReceive
                    pkt->subcmd_arg.arg2 = 0x00; // Count of the currecnt packet if the cmd is a series of packets.
                    buf[13] = 0x00;
                    buf[14] = 0x08; // 8: Last cmd packet, 0: More cmd packet should be  expected
                    buf[15] = 0x00; // Length of data after cmd header

                    buf[47] = mcu_crc8_calc(buf + 11, 36); //Without the last byte
                    res = hid_write(handle, buf, output_buffer_length - 1);
                    Sleep(200);
                    goto step5;
                }
                else if (buf2[49] == 0x2a)
                    break;
            }
            retries++;
            if (retries > 4 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 9) {
            res_get = 8;
            if (buf[62] == 0x4)
                FormJoy::myform1->txtBox_NFCTag->Text = String::Format("Mifare reading is not supported for now..");
            goto step9;
        }
    }

step9:
    // Disable MCU
    memset(buf, 0, sizeof(buf));
    auto hdr = (brcm_hdr *)buf;
    auto pkt = (brcm_cmd_01 *)(hdr + 1);
    hdr->cmd = 1;
    hdr->timer = timming_byte & 0xF;
    timming_byte++;
    pkt->subcmd = 0x22;
    pkt->subcmd_arg.arg1 = 0x00;
    res = hid_write(handle, buf, output_buffer_length);
    res = hid_read_timeout(handle, buf, sizeof(buf), 64);


    // Set input report to x3f
    while (1) {
        memset(buf, 0, sizeof(buf));
        auto hdr = (brcm_hdr *)buf;
        auto pkt = (brcm_cmd_01 *)(hdr + 1);
        hdr->cmd = 1;
        hdr->timer = timming_byte & 0xF;
        timming_byte++;
        pkt->subcmd = 0x03;
        pkt->subcmd_arg.arg1 = 0x3f;
        res = hid_write(handle, buf, output_buffer_length);
        int retries = 0;
        while (1) {
            res = hid_read_timeout(handle, buf, sizeof(buf), 64);
            if (*(u16*)&buf[0xD] == 0x0380)
                goto stepf;

            retries++;
            if (retries > 8 || res == 0)
                break;
        }
        error_reading++;
        if (error_reading > 7) {
            goto stepf;
        }
    }
stepf:
    if (res_get > 0)
        return res_get;

    return 0;
}

int device_connection(){
    if (check_connection_ok) {
        handle_ok = 0;
        // Joy-Con (L)
        if (handle = hid_open(0x57e, 0x2006, nullptr)) {
            handle_ok = 1;
            return handle_ok;
        }
        // Joy-Con (R)
        if (handle = hid_open(0x57e, 0x2007, nullptr)) {
            handle_ok = 2;
            return handle_ok;
        }
        // Pro Controller
        if (handle = hid_open(0x57e, 0x2009, nullptr)) {
            handle_ok = 3;
            return handle_ok;
        }
        // Nothing found
        else {
            return 0;
        }
    }
    return handle_ok;
}

[STAThread]
int Main(array<String^>^ args) {
    check_connection_ok = true;
    while (!device_connection()) {
        if (MessageBox::Show(
            L"The device is not paired or the device was disconnected!\n\n" +
            L"To pair:\n  1. Press and hold the sync button until the leds are on\n" +
            L"  2. Pair the Bluetooth controller in Windows\n\nTo connect again:\n" +
            L"  1. Press a button on the controller\n  (If this doesn\'t work, re-pair.)\n\n" +
            L"To re-pair:\n  1. Go to 'Settings -> Devices' or Devices and Printers'\n" +
            L"  2. Remove the controller\n  3. Follow the pair instructions",
            L"CTCaer's Joy-Con Toolkit - Connection Error!",
            MessageBoxButtons::RetryCancel, MessageBoxIcon::Stop) == System::Windows::Forms::DialogResult::Cancel)
            return 1;
    }
    // Enable debugging
    if (args->Length > 0) {
        if (args[0] == "-d")
            enable_traffic_dump = true; // Enable hid_write/read logging to text file
        else if (args[0] == "-f")
            check_connection_ok = false;   // Don't check connection after the 1st successful one
    }

    timming_byte = 0x0;

    Application::EnableVisualStyles();
    Application::SetCompatibleTextRenderingDefault(false);

    CppWinFormJoy::FormJoy^  myform1 = gcnew FormJoy();

    Application::Run(myform1);

    return 0;
}
