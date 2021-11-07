using AutoMapper;
using System.Collections.Generic;

namespace MemeFolderN
{
    public class AutoMapperConfigurationFactory
    {
        private readonly List<Profile> profiles;

        public AutoMapperConfigurationFactory(List<Profile> profiles)
        {
            this.profiles = profiles;
        }

        public MapperConfiguration GetMapperConfiguration()
        {
            var cfg = new MapperConfiguration(opt =>
            {
                profiles.ForEach(p => opt.AddProfile(p));
            });

            return cfg;
        }
    }
}
