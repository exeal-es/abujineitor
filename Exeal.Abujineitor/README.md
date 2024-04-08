# Exeal.Abujineitor

Inyector de dependencias hecho desde cero, para aprender cómo funciona un injector de dependencias.

## Requisitos

1. Registrar una instancia como singleton
Cuando te pida el tipo, me devuelves esa instancia

2. Registrar un tipo sin dependencias
Cuando te pida ese tipo, me fabricas y devuelves una instancia

3. Registrar un tipo que tiene una dependencia
Cuando te pida ese tipo, tienes que fabricar todo el arbol de dependencias

4. Bindear una interfaz a un tipo concreto
Cuando te pida la interfaz, me devuelves el tipo concreto

5. Soportar scopes: Transtient y Singleton
