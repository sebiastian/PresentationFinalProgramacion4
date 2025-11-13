using Domain.Abstraction;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly UserManagerDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(UserManagerDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public List<T> GetAll() => _dbSet.ToList();
    
    public T GetById(int id) => _dbSet.Find(id);
    
    public bool Create(T entity) 
    { 
        _dbSet.Add(entity); 
        _context.SaveChanges(); 
        return true; 
    }
    
    public bool Update(T entity) 
    { 
        _dbSet.Update(entity); 
        _context.SaveChanges(); 
        return true; 
    }
    
    public bool Delete(int id) 
    { 
        var entity = _dbSet.Find(id); 
        if (entity == null) return false; 
        _dbSet.Remove(entity); 
        _context.SaveChanges(); 
        return true; 
    }
}