using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Permissions;

namespace FileManagerService
{
    public class FileManager : IFileManagerServiceAddChange, IFileManagerServiceRemove
    {

        public static string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "..\\Files\\"));
        public static string config = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "..\\FIM\\FimConfig.txt"));


        [PrincipalPermission(SecurityAction.Demand, Role="Managment")]
        public void AddFile(string name, byte[] signature, string text)
        {
            if (!File.Exists(Path.Combine(path, name)))
            {
                File.WriteAllText(Path.Combine(path, name), text + '\n' + Convert.ToBase64String(signature));
                File.AppendAllText(config, $"\r\n{name}");
            }
            else
            {
                throw new FaultException<FileExceptions>(new FileExceptions("Error: File already exist, you can just change it!"));
            }

        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Managment")]
        public void ChangeFile(string name, byte[] signature, string text)
        {
            if (File.Exists(Path.Combine(path, name)))
            {
                File.WriteAllText(Path.Combine(path, name), text + '\n' + Convert.ToBase64String(signature));
            }
            else
            {
                throw new FaultException<FileExceptions>(new FileExceptions("Error: File doesn't exist, you must add it first!"));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public void RemoveFile(string name)
        {

            if (File.Exists(Path.Combine(path, name)))
            {
                File.Delete(Path.Combine(path, name));
                //izvlacimo one koji nisu izbrisani i s njima popunjavamo config
                List<string> nonDeleted = File.ReadAllLines(config).Where(file => !file.Equals(name)).ToList();
                File.WriteAllLines(config, nonDeleted);
            }
            else
            {
                throw new FaultException<FileExceptions>(new FileExceptions("Error: File not deleted"));
            }
        }
    }
}
