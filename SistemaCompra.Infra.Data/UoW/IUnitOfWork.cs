using System;
namespace SistemaCompra.Infra.Data.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}