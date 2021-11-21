using AutoMapper;
using MemeFolderN.Common.DTOClasses;
using MemeFolderN.Common.Models;
using System.Linq;

namespace MemeFolderN.Data.AutoMapperProfiles
{
    public class MapperProfileDAL : Profile
    {
        public MapperProfileDAL()
        {
            CreateMap<Folder, FolderDTO>()
                .ForMember(dst => dst.ParentFolder, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Folder, Folder>()
                .ForMember(dst => dst.Folders, opt => opt.Ignore())
                .ForMember(dst => dst.Memes, opt => opt.Ignore());

            CreateMap<Meme, MemeDTO>()
                .ForMember(dst => dst.ParentFolder, opt => opt.Ignore())
                .ForMember(dst => dst.TagGuids, opt => opt.MapFrom(src => src.TagNodes.Select(tn => tn.MemeTagId)))
                .ForMember(dst => dst.Tags, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Meme, Meme>()
               .ForMember(dst => dst.TagNodes, opt => opt.Ignore());

            CreateMap<MemeTag, MemeTagDTO>()
                .ReverseMap();

            CreateMap<MemeTag, MemeTag>();

            CreateMap<MemeTagNode, MemeTagNodeDTO>()
                .ReverseMap();
        }
    }
}
