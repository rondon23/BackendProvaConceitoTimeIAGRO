Um cliente tem necessidade de buscar livros em um catálogo. Esse cliente quer ler e buscar esse catálogo de um arquivo JSON, e esse arquivo não pode ser modificado. Então com essa informação, é preciso desenvolver:

- Criar uma API para buscar produtos no arquivo JSON disponibilizado.
- Que seja possível buscar livros por suas especificações(autor, nome do livro ou outro atributo)
- É preciso que o resultado possa ser ordenado pelo preço.(asc e desc)
- Disponibilizar um método que calcule o valor do frete em 20% o valor do livro.

Será avaliado no desafio:

- Organização de código;
- Manutenibilidade;
- Princípios de orientação à objetos;
- Padrões de projeto;
- Teste unitário

Para nos enviar o código, crie um fork desse repositório e quando finalizar, mande um pull-request para nós.

O projeto deve ser desenvolvido em C#, utilizando o .NET Core 3.1 ou superior.

Gostaríamos que fosse evitado a utilização de frameworks, e que tivesse uma explicação do que é necessário para funcionar o projeto e os testes.

## DESCRITIVO DOS ENDPOINTS DESENVOLVIDOS

## GET /BookStore/GetBookById

Endpoint para obter a lista de livros.

### Parâmetros de Consulta

| Parâmetro | Tipo   | Descrição              |
| --------- | ------ | ---------------------- |
| `id`    | número | id do livro      |

### Exemplo de Requisição

```http
GET /api/BookStore/GetBookById/1
```

### Exemplo de Resposta

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
  "id": 1,
  "name": "Journey to the Center of the Earth",
  "price": 10,
  "specifications": {
    "Originally published": "November 25, 1864",
    "Author": "Jules Verne",
    "Page count": 183,
    "Illustrator": [
      "Édouard Riou"
    ],
    "Genres": [
      "Science Fiction",
      "Adventure fiction"
    ]
  }
}
```

### Códigos de Status

| Código | Descrição          |
| ------ | ------------------ |
| 200    | OK                 |
| 400    | Bad Request        |
| 401    | Unauthorized       |
| 404    | Not Found          |
| 500    | Internal Server Error |

### Exemplo de Erro

```http
HTTP/1.1 404 Not Found
Content-Type: application/json

{
  "no record found with that id!"
}
```

## POST /api/BookStore/GetBookByParameters

Endpoint para pesquisar um livro por parametros.

### Corpo da Requisição

O corpo da requisição deve ser um objeto JSON com os seguintes campos:

| Campo    | Tipo   | Descrição           |
| -------- | ------ | ------------------- |
| `name`   | string | Nome do livro     |
| `price`  | string | Preço do livro   |
| `originally_published`    | number | Data de publicação    |
| `author`    | number | autor   |
| `page_count`    | number | numero de página  |
| `illustrator`    | number | Nome do ilustrador   |
| `genres`    | number | Gênero de livros    |
| `orderAsc`    | number | Ordenação   |

### Exemplo de Requisição

```http
POST /api/BookStore/GetBookByParameters
Content-Type: application/json

{
  "name": "",
  "price": 0,
  "originally_published": "",
  "author": "Jules Verne",
  "page_count": 0,
  "illustrator": "",
  "genres": "",
  "orderAsc": true
}
```

### Exemplo de Resposta

```http
HTTP/1.1 200 Ok
Content-Type: application/json

 {
    "id": 1,
    "name": "Journey to the Center of the Earth",
    "price": 10,
    "specifications": {
      "Originally published": "November 25, 1864",
      "Author": "Jules Verne",
      "Page count": 183,
      "Illustrator": [
        "Édouard Riou"
      ],
      "Genres": [
        "Science Fiction",
        "Adventure fiction"
      ]
    }
  }
```

### Exemplo de Erro

```http
HTTP/1.1 400 Bad Request
Content-Type: application/json

{
  "Nothing was found with these parameters!"
}
```
### Códigos de Status

| Código | Descrição          |
| ------ | ------------------ |
| 200    | OK                 |
| 400    | Bad Request        |
| 404    | Not Found          |
| 500    | Internal Server Error |

## GET /BookStore/shipping-cost

Endpoint para calcular o custo de frete de um livro.

### Parâmetros de Consulta

| Parâmetro | Tipo   | Descrição              |
| --------- | ------ | ---------------------- |
| `id`    | número | id do livro      |

### Exemplo de Requisição

```http
GET /api/BookStore/shipping-cost/1
```

### Exemplo de Resposta

```http
HTTP/1.1 200 OK
Content-Type: application/json

Shipping cost is: 2,00
```

### Códigos de Status

| Código | Descrição          |
| ------ | ------------------ |
| 200    | OK                 |
| 400    | Bad Request        |
| 401    | Unauthorized       |
| 404    | Not Found          |
| 500    | Internal Server Error |

### Exemplo de Erro

```http
HTTP/1.1 404 Not Found
Content-Type: application/json

{
  "book not found"
}
```