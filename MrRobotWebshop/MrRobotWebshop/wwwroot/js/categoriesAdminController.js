function drawHome() {
  $.ajax({
    type: "GET",
    url: "https://localhost:44316/api/categories",
    dataType: "json",
    success: function (products) {
      $("#guiContent").empty();

      console.log(products);

      $("#guiContent").append(
        $("<table/>")
          .attr({
            class: "table",
            id: "categoryTable",
          })
          .append(
            $("<thead/>").append(
              $("<tr/>").append(
                $("<th/>").text("Category Id"),
                $("<th/>").text("Category Name"),
                $("<th/>").text("Product Count"),
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
            $("<td/>").text(product.productCount),
            $("<td/>").append(
              $("<button/>")
                .attr({
                  type: "button",
                  class: "btn btn-danger",
                  id: "deleteCategoryButton",
                  categoryId: product.categoryId,
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
          url:
            "https://localhost:44316/api/categories/" +
            $(this).attr("categoryId"),
          success: function (response) {
            console.log(response);
          },
        });

        $(this.parentElement.parentElement).remove();
      });

      $("#guiContent").append(
        $("<button/>")
          .attr({
            type: "button",
            class: "btn btn-success",
            id: "addCategoryButton",
          })
          .text("Add category")
      );
    },
  });
}

function drawAddCategoryForm() {
  $("#guiContent").append(
    $("<form/>")
      .attr({
        id: "popupForm",
        class: "border border-dark",
        style:
          "width: 30em; height: 17em; padding: 1em; position: absolute; left: 40em; top: 20em; background-color: white;",
      })
      .append(
        $("<h3/>")
          .attr("style", "margin-bottom: 1em")
          .text("Input new category:"),
        $("<div/>")
          .attr("class", "form-group")
          .append(
            $("<label/>").attr("for", "categoryName").text("Category name:"),
            $("<input/>").attr({
              type: "text",
              class: "form-control",
              name: "inputCategoryName",
              placeholder: "Enter category name:",
              required: true,
            })
          ),
        $("<button/>")
          .attr({
            type: "submit",
            class: "btn btn-primary",
          })
          .text("Add category"),
        $("<i/>").attr({
          class: "fas fa-window-close fa-2x",
          id: "quitCategoryButton",
          style: "position: absolute; left: 447px; bottom: 242px; color: red;",
        })
      )
  );
}

$(document).on("mouseenter", "[id = quitCategoryButton]", function () {
  $(this).css("color", "darkred");
});

$(document).on("mouseleave", "[id = quitCategoryButton]", function () {
  $(this).css("color", "red");
});
