using System;
using System.Collections.Generic;
using System.Linq;
using Testing.Abstraction;
using Testing.Models;
using Testing.Resources;

namespace Testing
{
    public class DataResources : IDataResources
    {
        IEnumerable<bool> _booleans = new[] {true, false};

        IEnumerable<char> _chars;
        IEnumerable<GenderTypes> _dataGenders;
        IEnumerable<string> _emailTypes;
        IEnumerable<char> _letters;
        IEnumerable<char> _numbers;

        IEnumerable<string> _personFirstNamesFemale;
        IEnumerable<string> _personFirstNamesMale;
        IEnumerable<string> _personLastNames;

        IEnumerable<string> _webDomains;

        #region IDataResources Members

        public virtual IEnumerable<bool> Booleans
        {
            get { return _booleans; }
        }

        public virtual IEnumerable<char> Chars
        {
            get
            {
                return _chars ?? (
                                     _chars = TextResources.Chars.ToCharArray()
                                 );
            }
        }

        public virtual IEnumerable<char> Letters
        {
            get
            {
                return _letters ?? (
                                       _letters = TextResources.Letters.ToCharArray()
                                   );
            }
        }

        public virtual IEnumerable<char> Numbers
        {
            get
            {
                return _numbers ?? (
                                       _numbers = TextResources.Numbers.ToCharArray()
                                   );
            }
        }

        public virtual IEnumerable<string> PersonFirstNamesMale
        {
            get
            {
                return _personFirstNamesMale ??
                       (_personFirstNamesMale = PersonResources.FirstNamesMale.Split('\n'));
            }
        }

        public virtual IEnumerable<string> PersonFirstNamesFemale
        {
            get
            {
                return _personFirstNamesFemale ??
                       (_personFirstNamesFemale = PersonResources.FirstNamesFemale.Split('\n'));
            }
        }

        public virtual IEnumerable<GenderTypes> DataGenders
        {
            get
            {
                return _dataGenders ??
                       (_dataGenders = Enum.GetValues(typeof (GenderTypes)).Cast<GenderTypes>());
            }
        }

        public virtual IEnumerable<string> PersonLastNames
        {
            get
            {
                return _personLastNames ??
                       (_personLastNames = PersonResources.LastNames.Split('\n'));
            }
        }

        public virtual IEnumerable<string> WebDomains
        {
            get
            {
                return _webDomains ??
                       (_webDomains = WebResources.Domains.Split('\n'));
            }
        }

        public virtual IEnumerable<string> EmailTypes
        {
            get
            {
                return _emailTypes ??
                       (_emailTypes = WebResources.EmailTypes.Split('\n'));
            }
        }

        #endregion
    }
}