using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeFolderN.Extentions.Services
{
    public interface IUserSettingsService
    {
        string RootFolderPath { get; set; }
    }
}
