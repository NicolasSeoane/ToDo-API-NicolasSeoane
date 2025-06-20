# TodoApi - Nicolas Seoane

API para la gestión de tareas y listas de tareas, desarrollada con .NET 8.

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- (Opcional) [Visual Studio Code](https://code.visualstudio.com/) 
 
## Instalación

### 1. Clona el repositorio:

```bash
git clone https://github.com/NicolasSeoane/ToDo-API-NicolasSeoane.git
cd todo-api

```

### 2. Configurar la cadena de conexión de la base de datos
Edita el archivo appsettings.Development.json (dentro de appsettings.json)

```json
"ConnectionStrings": {
  "LocalConnection": "Server=Pc-Nicolas\\SQLEXPRESS;Database=TodoApiDB;Integrated Security=True;TrustServerCertificate=True"
}
```

### 3. Restaurar paquetes y compilar
```bash
dotnet restore
dotnet build
```

### 4. Levantar la API
```bash
dotnet run --project TodoApi
```


## Endpoints disponibles
La API expone los siguientes endpoints (accedé también vía Swagger):
```bash
GET     /api/items
GET     /api/items/{id}
POST    /api/items
PUT     /api/items/ChangeDescription
PUT     /api/items/MarkAsCompleted
DELETE  /api/items/{id}

GET     /api/todolists
GET     /api/todolists/{id}
POST    /api/todolists
PUT     /api/todolists/{id}
DELETE  /api/todolists/{id}
```
Swagger UI: http://localhost:7027/swagger
