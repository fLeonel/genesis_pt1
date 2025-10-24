## La Cazuela Chapina – Backend API

Sistema modular para la gestión de productos, ventas, recetas y análisis inteligente de demanda.

---

### Tecnologías

- **.NET 9 / C#**
- **Entity Framework Core + SQLite**
- **Minimal APIs**
- **Swagger / OpenAPI**
- **OpenRouter + GPT-4o-mini (análisis con IA)**
- **Arquitectura limpia:** Domain, Application, Infrastructure, Api

---

### Estructura del proyecto

```
src/
├── CazuelaChapina.Domain         → Entidades principales (Producto, Receta, Venta, etc.)
├── CazuelaChapina.Application    → UseCases, Commands y Queries
├── CazuelaChapina.Infrastructure → Persistencia (EF Core + SQLite)
└── CazuelaChapina.Api            → API REST + Swagger
```

---

### Configuración

#### 1. Variables de entorno

Crear un archivo `.env` en la raíz del proyecto con el siguiente contenido:

```env
OPEN_KEY=sk-or-v1-xxxxxxxxxxxxxxxxxxxxxxxx
MODEL_ID=openai/gpt-4o-mini
```

Si se utiliza **DeepSeek**, cambiar el modelo por:

```env
MODEL_ID=deepseek/deepseek-r1
```

---

#### 2. Migraciones y base de datos

Ejecutar el siguiente comando para aplicar las migraciones:

```bash
dotnet ef database update \
  --startup-project src/CazuelaChapina.Api \
  --project src/CazuelaChapina.Infrastructure
```

---

#### 3. Ejecutar la API

Iniciar el servidor con:

```bash
dotnet run --project src/CazuelaChapina.Api
```

La API estará disponible en:

```
http://localhost:5009/swagger
```

---

### Inteligencia de Ventas (IA)

El endpoint `/api/ventas/insights` genera análisis automáticos basados en el historial de ventas.

**Ejemplo de respuesta:**

```json
{
  "recomendacion": "Basado en el historial de ventas, se recomienda aumentar la producción de tamales en diciembre por alta demanda estacional."
}
```

---

### Entidades principales

- **Producto:** Contiene categoría, unidad de medida, precio y atributos.
- **Receta:** Lista de materiales e ingredientes (subproductos).
- **Venta:** Incluye detalles, cliente, método de pago y totales.
- **Combo:** Productos agrupados para venta conjunta.
- **Categoría:** Clasificación de productos.

---

### LLM Service

El sistema utiliza `LlmService.cs` para la comunicación con modelos de lenguaje a través de **OpenRouter API**.
Permite analizar datos y generar reportes inteligentes en lenguaje natural.

---

### Endpoints destacados

| Ruta                       | Descripción                    |
| -------------------------- | ------------------------------ |
| `GET /api/productos`       | Listado de productos           |
| `GET /api/ventas`          | Historial de ventas            |
| `GET /api/ventas/insights` | Análisis inteligente de ventas |
| `POST /api/recetas`        | Crear nueva receta             |
| `GET /swagger`             | Documentación completa         |
