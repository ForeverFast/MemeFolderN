using MemeFolderN.Core.Models;
using MemeFolderN.EntityFramework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MemeFolderN.UnitTests.DataServicesTests
{
    public class MemeDataServiceTests
    {
        private readonly IMemeDataService memeDataService;

        [Fact]
        public async void AddAndGetNewMeme()
        {
            // Arrange
            Meme newMeme = GetSingleMeme();

            // Act
            Meme dbCreatedMeme = await memeDataService.Create(newMeme);

            // Assert
            Assert.NotNull(dbCreatedMeme);
            Assert.NotNull(await memeDataService.GetById(dbCreatedMeme.Id));

            _ = await memeDataService.DeleteAllMemes();
        }

        [Fact]
        public async void DeleteMeme()
        {
            // Arrange
            Meme dbCreatedMeme = await memeDataService.Create(GetSingleMeme());
            Guid guid = dbCreatedMeme.Id;

            // Act
            bool result = await memeDataService.Delete(guid);

            // Assert
            Assert.True(result);
            Assert.Null(await memeDataService.GetById(guid));

            _ = await memeDataService.DeleteAllMemes();
        }

        [Fact]
        public async void UpdateMeme()
        {
            // Arrange
            Meme newMeme = GetSingleMeme();
            Meme dbCreatedMeme = await memeDataService.Create(newMeme);

            // Act
            string newPropValue = "test2";
            dbCreatedMeme.Title = newPropValue;
            Meme dbUpdatedMeme = await memeDataService.Update(dbCreatedMeme.Id, dbCreatedMeme);

            // Assert
            Assert.NotNull(dbUpdatedMeme);
            Assert.True(dbUpdatedMeme.Id == dbCreatedMeme.Id);
            Assert.True(dbUpdatedMeme.Title == newPropValue);

            _ = await memeDataService.DeleteAllMemes();
        }

        [Fact]
        public async void CreateMultipleMemes()
        {
            // Arrange
            List<Meme> memes = GetMultupleMemes();

            // Act
            IEnumerable<Meme> dbCreatedMemes = await memeDataService.CreateRange(memes.ToArray());

            // Assert
            Assert.True(memes.Count == dbCreatedMemes.Count());

            _ = await memeDataService.DeleteAllMemes();
        }

        [Fact]
        public async void DeleteMultipleMemes()
        {
            // Arrange
            List<Meme> memes = GetMultupleMemes();
            IEnumerable<Meme> dbCreatedMemes = await memeDataService.CreateRange(memes.ToArray());

            // Act
            bool result = await memeDataService.DeleteRange(memes.ToArray());

            // Assert
            Assert.True(result);

            _ = await memeDataService.DeleteAllMemes();
        }

        #region Вспомогательные методы

        private Meme GetSingleMeme()
        {
            Meme memeEntity = new Meme()
            {
                Title = "test1",
                ImagePath = "test1Path"
            };

            return memeEntity;
        }

        private List<Meme> GetMultupleMemes()
        {
            List<Meme> memes = new List<Meme>();
            for (int i = 0; i < 15; i++)
            {
                Meme memeEntity = new Meme()
                {
                    Title = $"test{i + 1}",
                    ImagePath = $"test{i + 1}Path"
                };
            }
            return memes;
        }

        #endregion

        public MemeDataServiceTests()
        {
            memeDataService = new MemeDataService();
        }
    }
}
