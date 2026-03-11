# 🖥️ PanelUserInventoryManagement

Panel web para el sistema de control de inventario. Permite a los usuarios gestionar salidas de inventario, consultar listados y registrar recepciones, consumiendo la API REST de inventario.

## 🛠️ Tecnologías

- .NET 8
- Blazor WebAssembly
- Radzen Blazor Components
- Bootstrap 5
- JWT Authentication (cliente)

## 📄 Páginas y funcionalidades

### 🔐 Login
- Autenticación de usuario contra la API
- Manejo de errores con mensaje de respuesta del servidor
- Redirección automática según estado de autenticación

### 📤 Registrar Salida de Inventario
- Acceso exclusivo para usuarios con rol **Jefe de Bodega**
- Selección de sucursal destino
- Selección de producto con búsqueda
- Ingreso de cantidad y agregar al grid de previsualización
- Muestra costo unitario estimado y subtotal estimado por producto
- Muestra total estimado de la salida antes de confirmar
- Registro de la salida contra la API con resultado en pantalla
- Manejo de errores: inventario insuficiente, límite L 5,000 por sucursal

### 📋 Listado de Salidas
- Listado de todas las salidas de inventario
- Filtros por rango de fechas y sucursal destino
- Columnas: número de salida, fecha, unidades totales, costo total, estado, recibido por y fecha de recepción
- Selección de fila para marcar una salida como recibida
- Botón **Marcar como Recibida** visible solo para salidas con estado `Enviada a Sucursal`
