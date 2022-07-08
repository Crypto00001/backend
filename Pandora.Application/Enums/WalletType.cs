using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.Enums
{
    public enum WalletType
    {
        [Description("1LBR8vNcxeGbdk3FpwvsADc4adYTNs7LVQ")]
        Bitcoin = 1,
        [Description("0xF4B8Aa66BEF1Bd61e8472526E88cf5A9781d5975")]
        Ethereum = 2,
        [Description("LTWisZwMEr6iEdLM3hAUdGrL1viN1ipCh3")]
        Litecoin = 3,
        [Description("t1U7dDLfd1sC4qyMmN5sfu5jLqrz2rVdVaC")]
        Zcash = 4,
        [Description("0xF4B8Aa66BEF1Bd61e8472526E88cf5A9781d5975")]
        Tether = 5
    }
}
