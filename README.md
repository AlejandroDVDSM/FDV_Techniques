# FDV_Techniques

## 1. Aplicar un fondo con scroll a tu escena utilizando la siguientes técnicas: 

### _a. La cámara está fija, el fondo se va desplazando en cada frame. Se usan dos fondos. Uno de ellos lo va viendo la cámara en todo momento, el otro está preparado para el momento en que se ha avanzado hasta el punto en el que la vista de la cámara ya no abarcaría el fondo inicial. Por tanto, se va actualizando en todo momento la posición de los dos fondos, haciéndolos avanzar hacia la izquierda. Cuando la cámara alcanza el límite, se debe intercambiar el rol de los fondos._

Primero añadimos un GameObject de tipo `Quad` en la escena que representará nuestro fondo. Para ello, pinchamos en la pestaña _"Inspector"_ y pulsamos la opción `3D Object > Quad`. Ahora, crearemos un material y se cambiará su tipo de shader a `Unlit/Texture` para poder asignar la imagen que deseemos que tenga el fondo como textura.

![image](https://github.com/user-attachments/assets/6c67205b-8348-4122-a5c1-729bc3f45c90)

Para terminar con la creación de nuestro fondo, arrastramos y soltamos el material al objeto de tipo `Quad`.

![image](https://github.com/user-attachments/assets/b4338509-6cc3-48bd-9f86-ee779a3e01d4)

Añadimos un segundo fondo para comenzar con la lógica de _scroll_.

![image](https://github.com/user-attachments/assets/8c473e98-ccc6-4f45-959c-b930a484745d)

A continuación, creamos un script que tenga una referencia para cada fondo, para la cámara empleada en el juego y otras variables más que nos ayudarán a conseguir que el fondo se mueva.

```cs
public class ScrollingBackgroundA : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;
    
    [SerializeField] private Renderer _backgroundOnView;
    [SerializeField] private Renderer _auxBackground;

    [SerializeField] private CinemachineVirtualCamera _vcam;
    private float _horizontalMovement;
    private Vector3 _direction;
}

```

Creamos el método `Update()` y hacemos que el fondo se mueva infinitamente hacia la izquierda:

```cs
private void Update()
{
    // Move background to the left
    _direction = new Vector3(-1, 0, transform.position.z).normalized;
    transform.Translate(_direction * (_moveSpeed * Time.deltaTime));
    }
```

Luego, comprobamos si el fondo inicial se encuentra fuera del campo de visión de la cámara. Si es así, lo movemos a la derecha de nuestro fondo auxiliar, que ahora debería de estar siendo enfocado por la cámara.

```cs
private void Update()
{
    // Move background to the left
    _direction = Vector3.left.normalized;
    transform.Translate(_direction * (_moveSpeed * Time.deltaTime));

    if (_backgroundOnView.transform.position.x + _backgroundOnView.bounds.size.x < _vcam.transform.position.x)
    {
        var newBackgroundPos = _backgroundOnView.transform.position;
        newBackgroundPos.x = _auxBackground.transform.position.x + _auxBackground.bounds.size.x;
        _backgroundOnView.transform.position = newBackgroundPos;
        (_auxBackground, _backgroundOnView) = (_backgroundOnView, _auxBackground);
    }
}
```

![1  Ejercicio 1a](https://github.com/user-attachments/assets/a38d7097-87cd-4931-b71d-c8a3f0716dd4)

### _b. La cámara se desplaza a la derecha y el fondo está estático. Existe nuevamente un fondo de reserva, que pasa a verse cuando el avance de la cámara sobrepasa el límite. El fondo anterior deb ubicarse a continuación del otro para que esté preparado._

El script para este apartado es bastante similar al anterior. La única diferencia notable es que en vez de mover los fondos a la izquierda se eliminará la lógica de su movimiento, por lo que permanecerán estáticos y lo único que deberán hacer es reposicionarse cuando la cámara enfoque el siguiente fondo.

```cs
using Cinemachine;
using UnityEngine;

public class ScrollingBackgroundB : MonoBehaviour
{
    [SerializeField] private Renderer _backgroundOnView;
    [SerializeField] private Renderer _auxBackground;
    
    [SerializeField] private CinemachineVirtualCamera _vcam;

    private void Update()
    {
        if (_backgroundOnView.transform.position.x + _backgroundOnView.bounds.size.x < _vcam.transform.position.x)
        {
            var newBackgroundPos = _backgroundOnView.transform.position;
            newBackgroundPos.x = _auxBackground.transform.position.x + _auxBackground.bounds.size.x;
            _backgroundOnView.transform.position = newBackgroundPos;
            (_auxBackground, _backgroundOnView) = (_backgroundOnView, _auxBackground);
        }
    }
}
```

Además de la modificación al anterior script, se crea uno nuevo que moverá eternamente al jugador hacia la derecha.

```cs
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private float _horizontalMovement;
    private Vector3 _direction;
    
    void Update()
    {
        // Move player to the right
        _direction = Vector3.right.normalized;
        transform.Translate(_direction * (_moveSpeed * Time.deltaTime));
    }
}
```

![1  Ejercicio 1b](https://github.com/user-attachments/assets/a4382897-bf01-4808-a9b7-6ca4d3021689)


## 2. Aplicar un fondo a tu escena aplicando la técnica del desplazamiento de textura.

Lo primero que se deberá hacer será cambiar el `Wrap Mode` de nuestra textura a `Repeat`.

![image](https://github.com/user-attachments/assets/6eb51b54-39be-47bc-9df2-28b673c159c1)

Ahora, en vez de desplazar el fondo o la cámara, se desplazará la textura:

```cs
using UnityEngine;

public class ScrollingBackground2 : MonoBehaviour
{
    [SerializeField] private float _scrollSpeedX = 0.5f;
    
    private Renderer _renderer; 
    private Vector2 _offset = Vector2.right;
    
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _offset.x += _scrollSpeedX * Time.deltaTime;
        
        _renderer.material.SetTextureOffset(MainTex, _offset);
    }
}
```

![2  Ejercicio 2](https://github.com/user-attachments/assets/d50b6063-306a-4b14-9811-b3ea5da8ac33)

## 3. Parallax

### _a. Aplicar efecto parallax usando la técnica de scroll en la que se mueve continuamente la posición del fondo._

Creamos varios materiales que serán las distintas capas de nuestro fondo con efecto parallax y se las asignamos alobjeto de tipo `Quad`. Luego, hacemos uso del script que se empleó en el apartado 1a.


![3  Ejercicio 3a](https://github.com/user-attachments/assets/ccb0a72b-32ea-4e75-8631-bb2d15f5296b)


### _b. Aplicar efecto parallax actualizando el offset de la textura._

```cs
using UnityEngine;

public class ScrollingBackgroundParallaxB : MonoBehaviour
{
    [SerializeField] private float _scrollSpeedX;
    
    private Renderer _renderer;
    private Material[] _parallaxLayers;
    
    private Vector2 _offset = Vector2.right;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _parallaxLayers = _renderer.materials;
    }

    private void Update()
    {
        _offset.x = _scrollSpeedX * Time.deltaTime;
        
        for (int i = 0; i < _parallaxLayers.Length; i++)
            _parallaxLayers[i].SetTextureOffset(MainTex, _parallaxLayers[i].GetTextureOffset(MainTex) + _offset / (i + 1.0f));
    }
}
```

![3  Ejercicio 3b](https://github.com/user-attachments/assets/cd04954f-cf0e-4609-82bc-70c067af4aa9)

