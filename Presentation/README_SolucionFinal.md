# Trabajo Práctico Final - Resumen Técnico y Funcional

---

## 1. Arquitectura y Capas

La solución está organizada en **capas** siguiendo el patrón Clean Architecture, lo que permite separar responsabilidades y facilitar el mantenimiento y la escalabilidad.

### **Capas principales:**

- **Domain:**  
  Define las entidades del modelo de negocio (por ejemplo, `User`, `Photographer`, `Location`, `PhotoSession`).  
  Aquí se encuentran las clases que representan los datos y sus relaciones.

- **Contract:**  
  Contiene los DTOs (Data Transfer Objects) para requests y responses.  
  Permite separar los datos que se reciben/envían por la API de las entidades internas.

- **Application:**  
  Define la lógica de negocio y los contratos (interfaces) para servicios y repositorios.  
  Incluye:
  - `Abstraction`: Interfaces de repositorios y servicios.
  - `Service`: Implementaciones de servicios (ej: `UserService`, `LocationService`).
  - `Interfaces`: Interfaces de servicios para inyección y desacoplamiento.

- **Infrastructure:**  
  Implementa la persistencia de datos usando Entity Framework Core.  
  Incluye:
  - `Persistence`: El DbContext (`UserManagerDbContext`), repositorios concretos y el repositorio genérico.
  - `Repository`: Implementaciones de repositorios, incluyendo el genérico (`RepositoryBase<T>`) y los específicos.
  - `Migrations`: Migraciones generadas por EF Core para crear y actualizar la base de datos.

- **Presentation:**  
  Expone la API REST usando ASP.NET Core.  
  Incluye:
  - `Controllers`: Controladores para cada entidad.
  - `Program.cs`: Configuración de la aplicación, inyección de dependencias y middlewares.
  - `appsettings.json`: Configuración de la cadena de conexión y otros parámetros.

---

## 2. Funcionamiento General

1. **El usuario realiza una petición HTTP** (por ejemplo, crear un usuario).
2. **El controlador** recibe la petición y la delega al **servicio** correspondiente.
3. **El servicio** aplica la lógica de negocio y utiliza el **repositorio** para acceder a la base de datos.
4. **El repositorio** realiza la operación CRUD usando Entity Framework Core.
5. **El resultado** se transforma en un DTO de respuesta y se devuelve al usuario.

---

## 3. Detalles Técnicos Importantes

### **a) Patrón Genérico en Repositorios**

- Se implementa un repositorio genérico `RepositoryBase<T>` que centraliza las operaciones CRUD para cualquier entidad.
- Las interfaces específicas (`IUserRepository`, `ILocationRepository`, etc.) heredan de la interfaz genérica `IRepositoryBase<T>`.
- Los repositorios concretos (`UserRepository`, `LocationRepository`, etc.) heredan del genérico y pueden agregar métodos propios si lo necesitan.
- Ejemplo de uso:

```csharp
public class LocationRepository : RepositoryBase<Location>, ILocationRepository
{
    public LocationRepository(UserManagerDbContext context) : base(context) { }
    // Métodos específicos de Location si los necesitas
}
```

### **b) Inyección de Dependencias**

- Todos los servicios y repositorios se inyectan en el contenedor de dependencias en `Program.cs` usando `AddScoped`.
- Esto permite desacoplar la lógica de negocio de la persistencia y facilita el testing.
- Ejemplo:

```csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
```

### **c) Entity Framework Core y Migraciones**

- Se utiliza EF Core en modo Code First para definir el modelo y generar la base de datos.
- El contexto `UserManagerDbContext` define los DbSet y las relaciones entre entidades.
- Las migraciones (`Add-Migration`, `Update-Database`) permiten crear y actualizar la base de datos de forma automática.

### **d) DTOs y Separación de Responsabilidades**

- Los DTOs en la capa Contract aseguran que solo los datos necesarios se expongan o reciban por la API.
- Los servicios se encargan de mapear entre entidades y DTOs, manteniendo la lógica de negocio separada de la persistencia y la presentación.

### **e) Relaciones y Navegación**

- Las entidades están relacionadas mediante claves foráneas y propiedades de navegación.
- Por ejemplo, `PhotoSession` tiene referencias a `Location`, `User` (como cliente) y `Photographer`.
- Los repositorios usan `.Include()` para cargar las relaciones cuando es necesario.

---

## 4. Ejemplo de Flujo Completo

**Crear una nueva Location:**
1. El usuario envía un POST a `/api/Location` con los datos.
2. El controlador recibe el DTO y llama a `LocationService.CreateLocation`.
3. El servicio mapea el DTO a la entidad y llama a `LocationRepository.Create`.
4. El repositorio genérico agrega la entidad y guarda los cambios en la base de datos.
5. El servicio devuelve el resultado al controlador, que responde al usuario.

---

## 5. Archivos y Carpetas Clave

- `Domain/Entity/*.cs`: Entidades principales del modelo.
- `Application/Abstraction/IRepositoryBase.cs`: Interfaz genérica para repositorios.
- `Infrastructure/Persistence/Repository/RepositoryBase.cs`: Implementación genérica del repositorio.
- `Infrastructure/Persistence/UserManagerDbContext.cs`: Contexto de EF Core.
- `Presentation/Controllers/*.cs`: Controladores de la API.
- `Presentation/Program.cs`: Configuración principal de la aplicación.
- `Contract/Location/Request/*.cs` y `Contract/Location/Response/*.cs`: DTOs para Location.

---

## 6. ¿Cómo ampliar o mantener?

- Para agregar una nueva entidad, crea el modelo en Domain, el DTO en Contract, la interfaz en Application y el repositorio en Infrastructure heredando del genérico.
- Para cambiar la persistencia, solo modifica Infrastructure, el resto de la solución permanece igual.
- Para agregar lógica de negocio, extiende los servicios en Application.

---

## 7. Ventajas de la Solución

- **Escalabilidad:** Fácil de agregar nuevas entidades y funcionalidades.
- **Mantenibilidad:** Código desacoplado y fácil de modificar.
- **Reutilización:** El patrón genérico evita duplicación de código.
- **Testabilidad:** La inyección de dependencias facilita las pruebas unitarias.
- **Robustez:** La base de datos relacional y las migraciones aseguran integridad y evolución del modelo.

---

## 8. Recomendaciones Finales y Buenas Prácticas

- Mantén la separación de capas y responsabilidades.
- Usa DTOs para exponer solo los datos necesarios por la API.
- Aplica migraciones cada vez que cambies el modelo de datos.
- Realiza pruebas unitarias sobre los servicios y repositorios.
- Documenta los endpoints y la arquitectura para facilitar el mantenimiento.

---

**Esta estructura te permite tener una solución robusta, escalable y fácil de mantener, cumpliendo con los requisitos académicos y profesionales.**
