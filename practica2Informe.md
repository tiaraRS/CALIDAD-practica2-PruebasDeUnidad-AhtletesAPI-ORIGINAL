## AthletesController.GetAthletesAsync

### Código

```csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<ShortAthleteModel>>> GetAthletesAsync(int disciplineId)
{
    try
    {
        var athletes = await _athleteService.GetAthletesAsync(disciplineId);//1
        return Ok(athletes);//2
    }
    catch (NotFoundElementException ex)//3
    {
        return NotFound(ex.Message);//4
    }
    catch (Exception)//5
    {
        return StatusCode(StatusCodes.Status500InternalServerError, "Something happened.");//6
    }
}
```

### Grafo

```mermaid
graph TD
    I(I) --> 1(1)
    1 --> 2(2)
    1 --> 3(3)
    3 --> 4(4)
    3 --> 5(5)
    5 --> 6(6)
    2 --> F(F)
    4 --> F(F)
    6 --> F(F)
```

### Complejidad ciclo matica

Numero de regiones
$$
v(G) = R \\
v(G) = 3
$$

Numero de nodos y aristas
$$
v(G) = E - N + 2 \\
v(G) = 9 - 8 + 2
$$
  
Numero de decisiones
$$
v(G) = P + 1 \\
v(G) = 2 + 1
$$

### Casos de prueba

| | Camino   | Entrada   | TC | Salida  |
| --- | --- | --- | --- | --- |
| 1 | I 1 3 5 6 F | `disciplineId` valid or invalid (throw not expected exception) | `disciplineId = 87` | `Status Code: 500` |
| 2 | I 1 3 4 F | `disciplineId` invalid  | `disciplineId = 87` | `Status Code: 404` |
| 3 | I 1 2 F | `disciplineId` valid  | `disciplineId = 1` | `Status Code: 200` `[{},{},...]` |

Camino 1
```mermaid
graph TD
    I(I):::c1 --> 1{1}
    1 --> 2(2)
    1:::c1 --> 3{3}
    3:::c1 --> 4(4)
    3 --> 5(5)
    5:::c1 --> 6(6)
    2 --> F(F)
    4 --> F(F)
    6:::c1 --> F(F):::c1
classDef c1 fill:#F2274C, stroke:#F2274C;
```

Camino 2
```mermaid
graph TD
    I(I):::c2 --> 1{1}
    1 --> 2(2)
    1:::c2 --> 3{3}
    3:::c2 --> 4(4)
    3 --> 5(5)
    5 --> 6(6)
    2 --> F(F)
    4:::c2 --> F(F)
    6 --> F(F):::c2
classDef c2 fill:#2964D9, stroke:#2964D9;
```

Camino 3
```mermaid
graph TD
    I(I):::c3 --> 1{1}
    1 --> 2(2)
    1:::c3 --> 3{3}
    3 --> 4(4)
    3 --> 5(5)
    5 --> 6(6)
    2:::c3 --> F(F)
    4 --> F(F)
    6 --> F(F):::c3
classDef c3 fill:#B2A2FA, stroke:#B2A2FA;
```

### Pruebas unitarias

```csharp
//tc1
```

```csharp
//tc2
```