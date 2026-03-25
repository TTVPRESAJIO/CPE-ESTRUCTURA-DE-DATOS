using System;
using System.Drawing;
using System.Drawing.Imaging;

class Nodo
{
    public int Valor;
    public Nodo Izq, Der;

    public Nodo(int valor)
    {
        Valor = valor;
        Izq = Der = null;
    }
}

class Arbol
{
    public Nodo Raiz;

    // Método para insertar valores en el árbol
    public void Insertar(int valor)
    {
        Raiz = InsertarRec(Raiz, valor);
    }

    // Inserción recursiva (BST)
    private Nodo InsertarRec(Nodo raiz, int valor)
    {
        if (raiz == null)
            return new Nodo(valor);

        if (valor < raiz.Valor)
            raiz.Izq = InsertarRec(raiz.Izq, valor);
        else
            raiz.Der = InsertarRec(raiz.Der, valor);

        return raiz;
    }

    // Mostrar árbol en consola (forma jerárquica)
    public void MostrarArbol()
    {
        MostrarRec(Raiz, "", true);
    }

    private void MostrarRec(Nodo nodo, string espacio, bool ultimo)
    {
        if (nodo != null)
        {
            Console.WriteLine(espacio + (ultimo ? "└── " : "├── ") + nodo.Valor);
            espacio += ultimo ? "    " : "│   ";

            MostrarRec(nodo.Izq, espacio, false);
            MostrarRec(nodo.Der, espacio, true);
        }
    }

    // Método para dibujar el árbol en una imagen
    public void DibujarArbol()
    {
        Bitmap bmp = new Bitmap(1200, 800);
        Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);

        // posición inicial del árbol
        DibujarNodo(g, Raiz, 600, 50, 300);

        bmp.Save("arbol.png", ImageFormat.Png);
        Console.WriteLine("\nImagen generada: arbol.png");
    }

    // Método recursivo para dibujar cada nodo
    private void DibujarNodo(Graphics g, Nodo nodo, int x, int y, int offset)
    {
        if (nodo == null) return;

        // dibuja el nodo
        g.FillEllipse(Brushes.LightBlue, x - 15, y - 15, 30, 30);
        g.DrawEllipse(Pens.Black, x - 15, y - 15, 30, 30);
        g.DrawString(nodo.Valor.ToString(), new Font("Arial", 10), Brushes.Black, x - 10, y - 8);

        // dibuja hijo izquierdo
        if (nodo.Izq != null)
        {
            g.DrawLine(Pens.Black, x, y, x - offset, y + 80);
            DibujarNodo(g, nodo.Izq, x - offset, y + 80, offset / 2);
        }

        // dibuja hijo derecho
        if (nodo.Der != null)
        {
            g.DrawLine(Pens.Black, x, y, x + offset, y + 80);
            DibujarNodo(g, nodo.Der, x + offset, y + 80, offset / 2);
        }
    }
}

class Program
{
    static void Main()
    {
        Arbol arbol = new Arbol();

        // Datos más grandes para generar un árbol más completo
        int[] datos =
        {
            50,
            25, 75,
            10, 30, 60, 85,
            5, 15, 27, 35, 55, 65, 80, 90
        };

        // insertar datos en el árbol
        foreach (int x in datos)
            arbol.Insertar(x);

        // mostrar en consola
        Console.WriteLine("ARBOL EN CONSOLA:\n");
        arbol.MostrarArbol();

        // generar imagen del árbol
        arbol.DibujarArbol();
    }
}