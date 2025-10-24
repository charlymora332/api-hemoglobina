# 🩸 API Hemoglobina

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Status](https://img.shields.io/badge/status-active-success?style=for-the-badge)

**API REST para procesamiento y análisis de niveles de hemoglobina en pacientes**

---

## 📋 Descripción

API desarrollada en **.NET** que procesa lotes de pacientes y evalúa sus niveles de hemoglobina según rangos médicos establecidos por género. El sistema clasifica automáticamente los resultados y genera alertas personalizadas para cada paciente.

### Características Principales

- **Procesamiento por Lotes** - Analiza múltiples pacientes en una sola petición  
- **Validación Robusta** - Control de errores por paciente sin detener el proceso completo  
- **Clasificación Automática** - Determina niveles Bajo, Normal o Alto según género  
- **Totales Agrupados** - Resumen estadístico por género y tipo de alerta  
- **Arquitectura Limpia** - Separación por capas (Domain, Application)

---

## 🏗️ Arquitectura

```
RetoHemoglobina/
├── RetoHemoglobina.API/              # Capa de presentación
├── RetoHemoglobina.Application/       # Lógica de aplicación
│   ├── Services/
│   │   └── PacienteService.cs        # Servicio principal
│   ├── DTOs/                          # Data Transfer Objects
│   ├── IServices/                     # Interfaces
│   └── Helpers/                       # Utilidades
├── RetoHemoglobina.Domain/            # Capa de dominio
│   ├── Models/                        # Entidades
│   └── Common/                        # Enums y constantes
```

---

## 🚀 Instalación

### Requisitos
- .NET 6.0 o superior
- Visual Studio 2022 / VS Code / Rider

### Pasos

```bash
# Clonar repositorio
git clone https://github.com/HMBC03/api-hemoglobina.git
cd api-hemoglobina

# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run --project RetoHemoglobina.API
```

---

## 📡 Endpoint Principal

### `POST /api/pacientes/procesar`

Procesa un lote de pacientes y retorna resultados individuales con totales agrupados.

**Request Body:**
```json
[
  {
    "nombre": "Juan Pérez",
    "genero": 2,
    "nivel": 15.5
  },
  {
    "nombre": "María García",
    "genero": 1,
    "nivel": 11.2
  },
  {
    "nombre": "Carlos López",
    "genero": 2,
    "nivel": 18.5
  }
]
```

**Parámetros:**
- `nombre` (string): Nombre del paciente
- `genero` (int): 1 = Mujer, 2 = Hombre
- `nivel` (float): Nivel de hemoglobina en g/dL (debe estar entre 0 y 30)

**Response:**
```json
{
  "pacientes": [
    {
      "nombre": "Juan Pérez",
      "genero": 2,
      "nivel": 15.5,
      "idAlerta": 1,
      "alerta": "Normal"
    },
    {
      "nombre": "María García",
      "genero": 1,
      "nivel": 11.2,
      "idAlerta": 0,
      "alerta": "Nivel bajo de hemoglobina"
    },
    {
      "nombre": "Carlos López",
      "genero": 2,
      "nivel": 18.5,
      "idAlerta": 2,
      "alerta": "Nivel alto de hemoglobina"
    }
  ],
  "totales": {
    "mujerBajo": 1,
    "mujerNormal": 0,
    "mujerAlto": 0,
    "hombreBajo": 0,
    "hombreNormal": 1,
    "hombreAlto": 1
  },
  "excepcion": []
}
```

---

## 🛡️ Manejo de Errores

La API valida cada paciente individualmente y continúa procesando los demás aunque uno falle:

**Response con Errores:**
```json
{
  "pacientes": [
    {
      "nombre": "Juan Pérez",
      "genero": 2,
      "nivel": 15.5,
      "idAlerta": 1,
      "alerta": "Normal"
    }
  ],
  "totales": {
    "hombreNormal": 1
  },
  "excepcion": [
    {
      "id": 2,
      "mensaje": "Faltan campos obligatorios: Nombre, Genero"
    },
    {
      "id": 3,
      "mensaje": "Nivel 35 no válido para paciente 3 -> Ana Martínez."
    }
  ]
}
```

### Validaciones Implementadas

El servicio valida:
- Campos obligatorios (Nombre, Género, Nivel)
- Rango de nivel válido (0 < nivel ≤ 30)
- Género válido (1 o 2)
- Procesamiento independiente por paciente

---

## 🧪 Ejemplo de Uso

### C# / HttpClient
```csharp
var client = new HttpClient();
var pacientes = new List<PacienteRequestDTO>
{
    new() { Nombre = "Juan Pérez", Genero = 2, Nivel = 15.5f },
    new() { Nombre = "María García", Genero = 1, Nivel = 13.2f }
};

var json = JsonSerializer.Serialize(pacientes);
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync(
    "https://localhost:5001/api/pacientes/procesar", 
    content
);

var resultado = await response.Content.ReadFromJsonAsync<RespuestaGeneralDTO>();
```

### cURL
```bash
curl -X POST https://localhost:5001/api/pacientes/procesar \
  -H "Content-Type: application/json" \
  -d '[
    {"nombre":"Juan Pérez","genero":2,"nivel":15.5},
    {"nombre":"María García","genero":1,"nivel":13.2}
  ]'
```

---

## 🔧 Tecnologías

- **Framework**: .NET 6+
- **Lenguaje**: C# 10
- **Patrón**: Clean Architecture
- **Validación**: Custom validators

---

## 📦 Estructura de DTOs

```csharp
// Request
public class PacienteRequestDTO
{
    public string? Nombre { get; set; }
    public int? Genero { get; set; }    // 1=Mujer, 2=Hombre
    public float? Nivel { get; set; }    // g/dL
}

// Response
public class RespuestaGeneralDTO
{
    public List<ResultadoPaciente> Pacientes { get; set; }
    public TotalesDTO Totales { get; set; }
    public List<Excepcion> Excepcion { get; set; }
}

// Resultado individual
public class ResultadoPaciente
{
    public string Nombre { get; set; }
    public byte Genero { get; set; }
    public float Nivel { get; set; }
    public byte IdAlerta { get; set; }
    public string Alerta { get; set; }
}
```

---

## 🤝 Contribuir

1. Fork el proyecto
2. Crea tu rama (`git checkout -b feature/nueva-funcionalidad`)
3. Commit tus cambios (`git commit -m 'Agrega nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Abre un Pull Request

---

## 👨‍💻 Autores

Proyecto original: **LOS EXTRATERRESTRES YA TU SABE**  


**Abusadol** *Brrr*

---

## 📄 Licencia

Este proyecto está bajo la Licencia MIT.

---

⭐ Si te fue útil, dale una estrella al repositorio


