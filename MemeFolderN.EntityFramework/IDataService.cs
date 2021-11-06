using MemeFolderN.Core.DTOClasses;
using System;
using System.Threading.Tasks;

namespace MemeFolderN.EntityFramework
{
    public interface IDataService<T>
        where T : DomainObjectDTO
    {
        /// <summary>
        /// Получение сущности по Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<T> GetById(Guid guid);
        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> Add(T entity);
        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="guid">Идентификтор сущности</param>
        /// <param name="entity">Сущность с новыми, не сохранёнными, параметрами</param>
        /// <returns></returns>
        Task<T> Update(Guid guid, T entity);
    }
}
