using CMSXData.Models;

namespace CMSXRepo;

public abstract class BaseRepositorio
{
    protected readonly CmsxDbContext _db;

    protected BaseRepositorio(CmsxDbContext db)
    {
        _db = db;
    }
}
