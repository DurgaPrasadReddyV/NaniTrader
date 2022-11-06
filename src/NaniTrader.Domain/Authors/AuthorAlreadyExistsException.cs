using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace NaniTrader.Authors
{
    public class AuthorAlreadyExistsException : BusinessException
    {
        public AuthorAlreadyExistsException(string name)
            : base(NaniTraderDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
