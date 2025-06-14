# 📤 Envío Concurrente de Archivos y Mensajes por Puerto Serial

Este módulo gestiona el envío de archivos y mensajes a través de un puerto serial. Implementa una cola para archivos, prioriza el envío de mensajes y sincroniza el acceso al puerto usando un `lock`.

---

## 🔁 Cola para Archivos (`ConcurrentQueue`)

- Los archivos se encolan en `colaArchivos` (`ConcurrentQueue<ArchivoEnviando>`) usando el método `IniciaEnvioArchivo`.
- El método `ProcesadorSecuencialArchivos` procesa los archivos **uno por uno**, asegurando que **solo un archivo se envíe a la vez**.
- Cada archivo debe terminar completamente antes de iniciar el siguiente.

---

## 🚀 Mensajes con Prioridad (`EnviarMensajePrioritario`)

- Los mensajes se envían mediante `enviaMensaje`, que ejecuta `EnviarMensajePrioritario` en un `Task` separado.
- **No se encolan**, lo que les da **prioridad inmediata**.
- Se envían tan pronto como el puerto serial está libre.

---

## 🔒 Sincronización con `lockEnvio`

- Tanto archivos como mensajes usan el objeto `lockEnvio` para sincronizar el acceso a `sPuerto.Write`.
- Esto garantiza que **solo un hilo** acceda al puerto serial a la vez.
- Si un mensaje necesita enviarse mientras un archivo está en proceso, espera a que se libere el `lockEnvio`.
- Una vez libre, el mensaje se envía **completo**, y luego el archivo continúa desde donde se detuvo.

---

## 🧱 Estructura de Tramas

### ✉️ Mensajes
- Divididos en fragmentos de hasta **1018 bytes**.
- Cabecera `"M"` + longitud de 5 dígitos.
- Suelen ser cortos y se envían rápidamente.

### 📁 Archivos
- Se envían en dos fases:
  1. **Metadatos** (cabecera `"F"`).
  2. **Contenido** (cabecera `"A"`).
- El contenido se divide en múltiples fragmentos, por lo que su envío es más lento.

---

## ⚙️ Flujo de Priorización

```mermaid
sequenceDiagram
    participant ColaArchivos
    participant ProcesadorArchivos
    participant EnviaMensaje
    participant PuertoSerial

    ColaArchivos->>ProcesadorArchivos: Encola archivo
    loop Fragmento por fragmento
        ProcesadorArchivos->>PuertoSerial: lockEnvio + Enviar fragmento
        EnviaMensaje->>PuertoSerial: Espera lockEnvio
        PuertoSerial-->>EnviaMensaje: lock liberado
        EnviaMensaje->>PuertoSerial: Enviar mensaje
        PuertoSerial-->>ProcesadorArchivos: lock liberado
    end
