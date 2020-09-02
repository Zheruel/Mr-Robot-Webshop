function drawHome() {
  //Get all categories
  $.ajax({
    type: "GET",
    url: "https://localhost:44316/api/categories",
    success: function (categories) {
      $("#categoryList").empty();

      for (const category of categories) {
        $("#categoryList").append(
          $("<li/>")
            .attr({
              class: "nav-item",
              id: category.categoryId,
              navItemType: "category",
            })
            .append(
              $("<a/>")
                .attr("class", "nav-link")
                .text(
                  category.categoryName + " (" + category.productCount + ")"
                )
            )
        );
      }

      //Get all products
      $.ajax({
        type: "GET",
        url: "https://localhost:44316/api/products",
        dataType: "json",
        success: function (products) {
          drawProducts(products);
        },
      });
    },
  });
}

function sortByCategory(categoryId) {
  $.ajax({
    type: "GET",
    url: "https://localhost:44316/api/products/category/" + categoryId,
    success: function (products) {
      //draw filtered products
      drawProducts(products);

      //Clear menu
      $("#categoryList").empty();

      //Grab subcategories
      $.ajax({
        type: "GET",
        url: "https://localhost:44316/api/subcategories/category/" + categoryId,
        success: function (subCategories) {
          for (const subCategory of subCategories) {
            $("#categoryList").append(
              $("<li/>")
                .attr({
                  class: "nav-item",
                  id: subCategory.subCategoryId,
                  navItemType: "subCategory",
                })
                .append(
                  $("<a/>")
                    .attr("class", "nav-link")
                    .text(
                      subCategory.subCategoryName +
                        " (" +
                        subCategory.productCount +
                        ")"
                    )
                )
            );
          }
        },
      });
    },
  });
}

function drawProducts(products) {
  $("#guiContent").empty();

  $("#guiContent").append(
    $("<table/>")
      .attr({
        class: "table",
        id: "productTable",
      })
      .append(
        $("<thead/>").append(
          $("<tr/>").append(
            $("<th/>").text("Product Id"),
            $("<th/>").text("Product image"),
            $("<th/>").text("Product name"),
            $("<th/>").text("Product description"),
            $("<th/>").text("Product price")
          )
        )
      )
      .append("<tbody/>")
  );

  for (const product of products) {
    $("tbody").append(
      $("<tr/>").append(
        $("<td/>").text(product.productId),
        $("<td/>").append(
          $("<img/>").attr({
            src: "../images/" + product.imageUrl,
            class: "img-thumbnail",
            style: "width:100px;height:100px;",
          })
        ),
        $("<td/>").text(product.productName),
        $("<td/>").text(product.productDescription),
        $("<td/>").text(product.price + " kn")
      )
    );
  }

  $("#productTable").DataTable();
}

function sortBySubCategory(subCategoryId) {
  $.ajax({
    type: "GET",
    url: "https://localhost:44316/api/products/subcategory/" + subCategoryId,
    success: function (products) {
      //draw filtered products
      drawProducts(products);

      //Clear menu
      $("#categoryList").empty();
    },
  });
}