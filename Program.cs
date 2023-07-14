/*написать программу которая будет сериализировать и десиарилизировать xml*/

using System.Xml.Serialization;


if (!File.Exists("product_list.xml"))
{
    using (StreamWriter sw = new StreamWriter("product_list.xml"))
    {
        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
    }
}

int key;
List<Products> productList = new List<Products>();

Console.ForegroundColor = ConsoleColor.Green;

while (true)
{
    try
    {
        Console.WriteLine();
        Console.WriteLine("* Введите 1 чтобы добавить новый продукт в список.");
        Console.WriteLine("* Введите 2 чтобы отобразить список продуктов.");
        Console.WriteLine("* Введите 3 чтобы выйти из программы.");
        Console.WriteLine();
        Console.Write("Ваш выбор: ");
        key = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();

        if (key == 1)
        {
            try
            {
                string product_name;
                int product_count;
                Console.WriteLine();
                Console.Write("Введите название продукта: ");
                product_name = Console.ReadLine();
                Console.Write("Введите количество: ");
                product_count = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine($"Продукт {product_name} в количестве: {product_count} успешно добавлен в список.");
                Console.WriteLine();
                Products product = new Products(product_name, product_count);
                productList.Add(product);

                XmlSerializer serialization = new XmlSerializer(typeof(List<Products>));

                using (StreamWriter sw = new StreamWriter("product_list.xml"))
                {
                    serialization.Serialize(sw, productList);
                }
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Введено некоректное название или количество продукта.");
                Console.WriteLine();
            }
        }

        if (key == 2)
        {
            try
            {
                if (File.Exists("product_list.xml"))
                {
                    XmlSerializer deserialization = new XmlSerializer(typeof(List<Products>));
                    using (StreamReader sr = new StreamReader("product_list.xml"))
                    {
                        productList = (List<Products>)deserialization.Deserialize(sr);

                        foreach (Products product in productList)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Название: {product.Name}");
                            Console.WriteLine($"Количество: {product.Count}");
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Список продуктов пуст.");
                Console.WriteLine();
            }
        }

        if (key == 3)
        {
            break;
        }
    }
    catch
    {
        Console.WriteLine();
        Console.WriteLine("Введено некоректное значение.");
        Console.WriteLine();
    }
}

[Serializable]
[XmlRoot]
public class Products
{
    [XmlAttribute]
    public string Name { get; set; }

    [XmlElement]
    public int Count { get; set; }

    public Products()
    {

    }

    public Products(string name, int count)
    {
        Name = name;
        Count = count;
    }
}