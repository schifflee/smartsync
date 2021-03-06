﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bedrock.Common;

namespace SmartSync.Common
{
    using File = Bedrock.Common.File;
    using Directory = Bedrock.Common.Directory;

    public class ZipFile : File
    {
        public override string Name
        {
            get
            {
                string name = "/" + file.FullName;
                return name.Substring(name.LastIndexOf('/') + 1);
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
        public override Storage Storage
        {
            get
            {
                return storage;
            }
        }

        public override DateTime Date
        {
            get
            {
                return file.LastWriteTime.DateTime;
            }
            set
            {
                Update();
                file.LastWriteTime = new DateTimeOffset(value);
                storage.Modified = true;
            }
        }
        public override ulong Size
        {
            get
            {
                return (ulong)file.Length;
            }
        }
        public override uint Hash
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        internal ZipStorage storage;
        internal Directory parent;
        internal ZipArchiveEntry file;

        public ZipFile(ZipStorage storage, Directory parent, ZipArchiveEntry file)
        {
            this.storage = storage;
            this.parent = parent;
            this.file = file;
        }

        public override Stream Open(FileAccess access)
        {
            Update();
            return new ZipStream(storage, file.Open());
        }
        private void Update()
        {
            file = storage.Archive.GetEntry(file.FullName);
        }
    }
}