using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffmpegDemo
{
    public class ffmpegManager
    {
        Process p;

        public ffmpegManager()
        {
            Init();
        }

        /// <summary>
        /// 初始化操作
        /// </summary>
        private void Init()
        {
            p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            //启动程序
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
        }
        /// <summary>
        /// 录制视频
        /// </summary>
        /// <param name="fileName">保存视频的名称</param>

        public void RecordScreen()
        {

            //只录画面+无声音 @"ffmpeg -f gdigrab -i desktop -pix_fmt yuv420p D:\out.mp4";
            //录画面+声音： "ffmpeg -f gdigrab -i desktop -f dshow -rtbufsize 1G -i audio=\"virtual-audio-capturer\" -pix_fmt yuv420p D:\\out.mp4";
            //录画面 + 声音 + 麦克风："ffmpeg -f gdigrab -i desktop -f dshow -rtbufsize 1G -i audio=\"virtual-audio-capturer\" -f dshow -rtbufsize 100M -i audio=\"麦克风" -filter_complex amix=inputs=2 -pix_fmt yuv420p D:\\out.mp4";
            string command = "ffmpeg -f gdigrab -i desktop -f dshow -rtbufsize 1G -i audio=\"virtual-audio-capturer\" -f dshow -rtbufsize 100M -i audio=\"@device_cm_{33D9A762-90C8-11D0-BD43-00A0C911CE86}\\wave_{0ED29E85-6EB2-477C-BF34-FA62DFD00EAA}\" -filter_complex amix=inputs=2 -pix_fmt yuv420p -s 1920*1080 -b:v 3000k -preset superfast D:\\out.mp4";
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(command);

            p.StandardInput.AutoFlush = true;

        }

        //停止录制
        public void StopRecord()
        {

            //输入q命令停止录制
            p.StandardInput.WriteLine("q");

        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Console.WriteLine(e.Data);
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Console.WriteLine(e.Data);
            }
        }
    }
}
