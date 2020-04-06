using System;
using Topshelf;

namespace TplinkPowerlineFix
{
    class Program
    {
        public static void Main()
        {
            var rc = HostFactory.Run(x =>                                   //1
            {
                x.Service<PingService>(s =>                                   //2
                {
                    s.ConstructUsing(name => new PingService());                //3
                    s.WhenStarted(async pingService => await pingService.Start());
                    s.WhenStopped(pingService => pingService.Stop());    
                    //4
                                                                                      //5
                });
                x.RunAsLocalSystem();                                       //6

                x.SetDescription("https://www.youtube.com/watch?v=Al3kwmfzsOQ");                   //7
                x.SetDisplayName("Tplink Powerline Fix");                                  //8
                x.SetServiceName("Tplink.Powerline.Fix");                                  //9
            });                                                             //10

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  //11
            Environment.ExitCode = exitCode;
        }
    }
}
