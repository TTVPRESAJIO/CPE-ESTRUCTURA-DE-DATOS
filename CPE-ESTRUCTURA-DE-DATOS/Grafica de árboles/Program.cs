using System;
using System.Drawing;
using System.Drawing.Imaging;

class Nodo
{
    public int Valor;
    public Nodo Izquierda;
    public Nodo Derecha;

    public Nodo(int valor)
    {
        Valor = valor;
        Izquierda = Derecha = null;
    }
}

class ArbolBST
{
    public Nodo Raiz;

    public Nodo Insertar(Nodo raiz, int valor)
    {
        if (raiz == null)
            return new Nodo(valor);

        if (valor < raiz.Valor)
            raiz.Izquierda = Insertar(raiz.Izquierda, valor);
        else
            raiz.Derecha = Insertar(raiz.Derecha, valor);

        return raiz;
    }

    public bool Buscar(Nodo raiz, int valor)
    {
        if (raiz == null) return false;
        if (raiz.Valor == valor) return true;

        if (valor < raiz.Valor)
            return Buscar(raiz.Izquierda, valor);
        else
            return Buscar(raiz.Derecha, valor);
    }

    public int Minimo(Nodo raiz)
    {
        while (raiz.Izquierda != null)
            raiz = raiz.Izquierda;
        return raiz.Valor;
    }

    public int Maximo(Nodo raiz)
    {
        while (raiz.Derecha != null)
            raiz = raiz.Derecha;
        return raiz.Valor;
    }

    public Nodo Eliminar(Nodo raiz, int valor)
    {
        if (raiz == null) return raiz;

        if (valor < raiz.Valor)
            raiz.Izquierda = Eliminar(raiz.Izquierda, valor);
        else if (valor > raiz.Valor)
            raiz.Derecha = Eliminar(raiz.Derecha, valor);
        else
        {
            if (raiz.Izquierda == null)
                return raiz.Derecha;
            else if (raiz.Derecha == null)
                return raiz.Izquierda;

            raiz.Valor = Minimo(raiz.Derecha);
            raiz.Derecha = Eliminar(raiz.Derecha, raiz.Valor);
        }

        return raiz;
    }

    public void InOrden(Nodo raiz)
    {
        if (raiz != null)
        {
            InOrden(raiz.Izquierda);
            Console.Write(raiz.Valor + " ");
            InOrden(raiz.Derecha);
        }
    }

    public void PreOrden(Nodo raiz)
    {
        if (raiz != null)
        {
            Console.Write(raiz.Valor + " ");
            PreOrden(raiz.Izquierda);
            PreOrden(raiz.Derecha);
        }
    }

    public void PostOrden(Nodo raiz)
    {
        if (raiz != null)
        {
            PostOrden(raiz.Izquierda);
            PostOrden(raiz.Derecha);
            Console.Write(raiz.Valor + " ");
        }
    }

    public int Altura(Nodo raiz)
    {
        if (raiz == null) return -1;

        int izq = Altura(raiz.Izquierda);
        int der = Altura(raiz.Derecha);

        return Math.Max(izq, der) + 1;
    }

    public void Limpiar()
    {
        Raiz = null;
    }

    // ================= MOSTRAR EN CONSOLA =================
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

            MostrarRec(nodo.Izquierda, espacio, false);
            MostrarRec(nodo.Derecha, espacio, true);
        }
    }

    // ================= GENERAR IMAGEN =================
    public void DibujarArbol()
    {
        Bitmap bmp = new Bitmap(1200, 800);
        Graphics g = Graphics.FromImage(bmp);
        g.Clear(Color.White);

        DibujarNodo(g, Raiz, 600, 50, 300);

        bmp.Save("arbol.png", ImageFormat.Png);
        Console.WriteLine("Imagen generada: arbol.png");
    }

    private void DibujarNodo(Graphics g, Nodo nodo, int x, int y, int offset)
    {
        if (nodo == null) return;

        g.FillEllipse(Brushes.LightBlue, x - 15, y - 15, 30, 30);
        g.DrawEllipse(Pens.Black, x - 15, y - 15, 30, 30);
        g.DrawString(nodo.Valor.ToString(), new Font("Arial", 10), Brushes.Black, x - 10, y - 8);

        if (nodo.Izquierda != null)
        {
            g.DrawLine(Pens.Black, x, y, x - offset, y + 80);
            DibujarNodo(g, nodo.Izquierda, x - offset, y + 80, offset / 2);
        }

        if (nodo.Derecha != null)
        {
            g.DrawLine(Pens.Black, x, y, x + offset, y + 80);
            DibujarNodo(g, nodo.Derecha, x + offset, y + 80, offset / 2);
        }
    }
}

class Program
{
    static void Main()
    {
        ArbolBST arbol = new ArbolBST();
        int opcion, valor;

        do
        {
            Console.WriteLine("\n===== ARBOL BST =====");
            Console.WriteLine("1. Insertar");
            Console.WriteLine("2. Buscar");
            Console.WriteLine("3. Eliminar");
            Console.WriteLine("4. Recorridos");
            Console.WriteLine("5. Minimo y Maximo");
            Console.WriteLine("6. Altura");
            Console.WriteLine("7. Limpiar arbol");
            Console.WriteLine("8. Mostrar arbol en consola");
            Console.WriteLine("9. Generar imagen del arbol");
            Console.WriteLine("0. Salir");

            Console.Write("Opcion: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("Valor: ");
                    valor = int.Parse(Console.ReadLine());
                    arbol.Raiz = arbol.Insertar(arbol.Raiz, valor);
                    break;

                case 2:
                    Console.Write("Valor: ");
                    valor = int.Parse(Console.ReadLine());
                    Console.WriteLine(arbol.Buscar(arbol.Raiz, valor) ? "Encontrado" : "No encontrado");
                    break;

                case 3:
                    Console.Write("Valor: ");
                    valor = int.Parse(Console.ReadLine());
                    arbol.Raiz = arbol.Eliminar(arbol.Raiz, valor);
                    break;

                case 4:
                    Console.WriteLine("InOrden:");
                    arbol.InOrden(arbol.Raiz);
                    Console.WriteLine("\nPreOrden:");
                    arbol.PreOrden(arbol.Raiz);
                    Console.WriteLine("\nPostOrden:");
                    arbol.PostOrden(arbol.Raiz);
                    Console.WriteLine();
                    break;

                case 5:
                    Console.WriteLine("Min: " + arbol.Minimo(arbol.Raiz));
                    Console.WriteLine("Max: " + arbol.Maximo(arbol.Raiz));
                    break;

                case 6:
                    Console.WriteLine("Altura: " + arbol.Altura(arbol.Raiz));
                    break;

                case 7:
                    arbol.Limpiar();
                    Console.WriteLine("Arbol eliminado");
                    break;

                case 8:
                    arbol.MostrarArbol();
                    break;

                case 9:
                    arbol.DibujarArbol();
                    break;
            }

        } while (opcion != 0);
    }
}