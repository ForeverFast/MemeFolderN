using System;
using System.Collections.Generic;

namespace MemeFolderN.Core.DTOClasses
{
    public class MemeDTO : FolderObjectDTO
    {
        public DateTime AddingDate { get; set; }

        public string ImagePath { get; set; }

        public string MiniImagePath { get; set; }

        public List<MemeTagNodeDTO> Tags { get; set; }

        public MemeDTO() : base()
        {
            AddingDate = DateTime.Now;
            Tags = new List<MemeTagNodeDTO>();
        }
    }
}
