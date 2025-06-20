# 🧥 UniformProjectOmar

Sistema de gestión de uniformes para empleados desarrollado en **Blazor Server** y **Entity Framework Core 9**, con SQL Server como base de datos.

Permite llevar el control de artículos por tipo de empleado (sindicalizados / confianza), registrar movimientos de almacén y consultar entregas de forma sencilla y visual.

---

## 🛠️ Tecnologías Utilizadas

- ✅ Blazor Server (.NET 9)
- ✅ Entity Framework Core 9 (Database First)
- ✅ SQL Server
- ✅ Bootstrap 5
- ✅ JavaScript Interop (Toastr y SweetAlert)
- ✅ Git & GitHub

---

## 📦 Funcionalidades

- Gestión de artículos de almacén (CRUD)
- Registro de movimientos (entradas/salidas)
- Validación de datos con DataAnnotations
- Vistas y procedimientos almacenados desde SQL Server
- Notificaciones con Toastr
- Confirmaciones con SweetAlert
- Navegación modular con submenús

---

🧪 Requisitos
.NET 9 SDK

SQL Server Express o superior

Visual Studio / VS Code (opcional)

🧠 Notas Técnicas
Los IDs de las tablas que no usan IDENTITY se manejan manualmente desde C# con MAX(Id) + 1

Las cadenas como Descripción están validadas en tiempo real (case insensitive)

El proyecto está preparado para escalar y reutilizar componentes

🤝 Autor
Omar Quijano
Desarrollador Fullstack .NET & React
GitHub: @Ppomar
