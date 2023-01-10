using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using System.ServiceModel;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Permissions;

namespace FileManagerService
{
    public class FileManager : IFileManagerServiceAddChange, IFileManagerServiceRemove
    {

        public static string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "..\\Files\\"));

        [PrincipalPermission(SecurityAction.Demand, Role="Managment")]
        public void AddFile(string name, string text)
        {
            if (!File.Exists(Path.Combine(path, name)))
            {
                File.WriteAllText(Path.Combine(path, name), text);
            }
            else
            {
                throw new FaultException<FileExceptions>(new FileExceptions("Error: File not added"));
            }

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Managment")]

        public void ChangeFile(string name, string text)
        {
            if (File.Exists(Path.Combine(path, name)))
            {
                File.WriteAllText(Path.Combine(path, name), text);
            }
            else
            {
                throw new FaultException<FileExceptions>(new FileExceptions("Error: File not changed"));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]

        public void RemoveFile(string name)
        {
            if (!File.Exists(Path.Combine(path, name)))
            {
                File.Delete(Path.Combine(path, name));

            }
            else
            {
                throw new FaultException<FileExceptions>(new FileExceptions("Error: File not deleted"));
            }
        }
    }
}
