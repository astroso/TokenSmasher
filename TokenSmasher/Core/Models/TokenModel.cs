using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenSmasher.Checker.Models {
    public class TokenModel {
        public static List<string> Tokens = new List<string>();

        public static List<string> WorkingTokens = new List<string>();
        
        public static List<string> DeadTokens = new List<string>();
        
        public static List<string> LockedTokens = new List<string>();
    }
}
