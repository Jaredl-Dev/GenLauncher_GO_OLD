using System;
using System.IO;
using System.Text;

namespace GenLauncherNet
{
    public static class BinaryReaderExtension
    {
        public static string ReadFourCc(this BinaryReader reader, bool bigEndian = false)
        {
            var a = (char)reader.ReadByte();
            var b = (char)reader.ReadByte();
            var c = (char)reader.ReadByte();
            var d = (char)reader.ReadByte();

            return bigEndian
                ? new string(new[] { d, c, b, a })
                : new string(new[] { a, b, c, d });
        }
    }

    public static class BigHandler
    {
        public static bool IsBigArchive(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("Cannot find file " + filePath);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                {
                    using (var reader = new BinaryReader(fileStream, Encoding.ASCII, true))
                    {
                        try
                        {
                            //Special case for empty archives/ placeholder archives
                            if (reader.BaseStream.Length < 4)
                            {
                                var a = reader.ReadByte();
                                var b = reader.ReadByte();

                                if (a == '?' && b == '?')
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }

                            var fourCc = reader.ReadFourCc();
                            switch (fourCc)
                            {
                                case "BIGF":
                                    return true;

                                case "BIG4":
                                    return true;

                                default:
                                    return false;
                            }
                        }
                        catch
                        {
                            //TODO logger
                            return false;
                        }
                    }
                }
            }
            catch
            {
                //TODO logger
                return false;
            }
        }
    }
}
