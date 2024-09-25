using PayPalCheckoutSdk.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Payments
{
    public class PayPalClient
    {
        private static PayPalEnvironment Environment()
        {
            //return new SandboxEnvironment("AVq6eqGvxVhuZYpXQkhMD7MEBJt_AC2I1-vo1d7ZnKHwbAuyzXJiD7WLz0ElCsA0YAKFb3-dnoh8Ozff"
            //    , "EPnuJktJaWN9i3daSvEvmx35LcvUZH_q75RamXftFBE_oZ-7Ik_SPV2T5F2J_qB9lKmM9cSJBaQLpn87");

            return new LiveEnvironment("AdmD088x9qeL5RiK4mzxYNt6CcBqn1O-3MeELl5WvqWO4mXmhwxfwIiKucb1cNu0d4mgbsq4aL_4LU6H"
                , "ECmIScH5kW1Yb-I4XdzE2KDEwARiEMfiU0THIm66AHGGLLsThSShtV9DdzcuPM9zlSbz79TNysELneFX");

        }

        public static PayPalHttpClient Client()
        {
            return new PayPalHttpClient(Environment());
        }
    }
}
