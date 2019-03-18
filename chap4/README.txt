# Model Ŭ�������� getter/setter �ڵ� ���
- ���� ������ �ʿ��� �Ӽ��� getter�� setter�� ���� �����Ѵ�
- �� �ܿ��� get; set; �� ����� getter�� setter�� �ڵ����� ����Ѵ�.
```
        private string name;

        public string Name
        {
            get
            {
                return "hihi" + name;
            }
            set
            {
                name = value;
            }
        }

        public string Description
        { get; set; }
```

# ��ü �̴ϼȶ����� �� �͸� ���� ���
- ��ü�� �����Ǵ� ���ÿ� setter ������� �Ӽ� ������ �ٷ� �����ǵ��� �Ѵ�.
- var Ű���带 ���� �͸� ������ ��ü�� �����Ѵ�.
```
// �������� Product myProduct = new Product{ ... };

            var myProduct = new
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };
```

# LINQ ����ϱ�
- Ŭ������ �����ϴ� �����͸� �����ϱ� ���� SQL�� ������ �����̴�.
- ��ħǥ ǥ��� ������ ����Ͽ� ���� ����� ����� �� �ִ�.
- ������� ��� ���� ������ ��ǰ�� ã�µ�, 3���� ����� ��� ���� ���
```
// Dot Notation�� ����ϴ� ��� ---> SQL ������ �ణ �޶� ������ ����� �� �ִ�.

            Product[] products =
            {
                new Product {Name="Kayak", Category="Watersports", Price=275M },
                new Product {Name="Lifejacket", Category="Watersports", Price=48.95M },
                new Product {Name="Soccer ball", Category="Soccer", Price=19.50M },
                new Product {Name="Corner flag", Category="Soccer", Price=34.95M }
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                                    .Take(3)
                                    .Select(e => new { e.Name, e.Price });

            StringBuilder result = new StringBuilder();
            foreach (var fp in foundProducts)
                result.AppendFormat("Price : {0}", fp.Price);

// Dot Notation�� ������� �ʴ� ���

            var foundProducts2 = from match in products
                                 orderby match.Price descending
                                 select new { match.Name, match.Price };

	// SQL ���ǿ� ����� ����� ����ϱ� ��������
	// 3���� ����� ��� ���ؼ��� foreach���� count�� �̿��� 3���� �Ǵ� ���
	// break �ϵ��� �ؾ��ϴ� �������� �ִ�.
```
- ������ Join ������ ��ũ
  - https://stackoverflow.com/questions/1511833/linq-dot-notation-equivalent-for-join/1511857

# �񵿱� ó�� (async�� await Ű����)
- async Ű���带 �޼ҵ忡 ����ϸ�, �񵿱� ó�� �۾��� ��ٸ� �κп� await Ű���带 ����Ѵ�.
```
        public async static Task<long?> GetPageLength()
        {
            HttpClient clinet = new HttpClient();

            var httpMessage = await clinet.GetAsync("http://apress.com");

            // HTTP ��û�� �Ϸ�Ǳ⸦ ��ٸ��� ����
            // ���⼭ �ٸ� �۾��� ó���� �� �ִ�.

            return httpMessage.Content.Headers.ContentLength;
        }
```
