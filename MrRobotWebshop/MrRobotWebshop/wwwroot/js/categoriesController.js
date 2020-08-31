$.ajax({
  type: "GET",
  url: "https://localhost:44316/api/categories",
  dataType: "json",
  success: function (products) {
    $("#guiContent").empty();

    console.log(products);

    $("#guiContent").append(
      $("<table/>")
      .attr(
        {
          class: "table",
          id: "categoryTable"
        }
      )
      .append(
        $("<thead/>").append(
          $("<tr/>")
            .append(
              $("<th/>").text("Category Id"),
              $("<th/>").text("Category Name"),
              $("<th/>").text("Subcategory Count"),
              $("<th/>")
            )
        )
      )
      .append("<tbody/>")
    );

    for (const product of products) {
      $("tbody").append(
        $("<tr/>").append(
          $("<td/>").text(product.categoryId),
          $("<td/>").text(product.categoryName),
          $("<td/>").text(product.subCategoryCount),
          $("<td/>").append(
            $("<button/>")
              .attr({
                type: "button",
                class: "btn btn-danger",
                id: "deleteCategoryButton",
                categoryId: product.categoryId
              })
              .text("Delete")
          )
        )
      );
    }

    $("#categoryTable").DataTable();

    $("button[id='deleteCategoryButton']").click(function (e) { 
      e.preventDefault();
      $.ajax({
        type: "DELETE",
        url: "https://localhost:44316/api/categories/" + $(this).attr("categoryId"),
        success: function (response) {
          console.log(response);
        }
      });

      $(this.parentElement.parentElement).remove();
    });
  }
});