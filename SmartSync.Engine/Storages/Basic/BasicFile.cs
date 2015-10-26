﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSync.Engine
{
    public class BasicFile : File
    {
        public override string Name
        {
            get
            {
                return fileInfo.Name;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override Directory Parent
        {
            get
            {
                return parent;
            }
        }
        public override DateTime Date
        {
            get
            {
                return fileInfo.LastWriteTime;
            }
            set
            {
                fileInfo.LastWriteTime = value;
            }
        }
        public override uint Hash
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private FileInfo fileInfo;
        private Directory parent;

        public BasicFile(FileInfo fileInfo, Directory parent)
        {
            this.fileInfo = fileInfo;
            this.parent = parent;
        }

        public override Stream Open(FileAccess access)
        {
            FileShare share = FileShare.None;

            switch (access)
            {
                case FileAccess.Read: share = FileShare.ReadWrite; break;
                case FileAccess.Write: share = FileShare.Read; break;
                case FileAccess.ReadWrite: share = FileShare.Read; break;
            }

            return fileInfo.Open(FileMode.Open, access, share);
        }
    }
}