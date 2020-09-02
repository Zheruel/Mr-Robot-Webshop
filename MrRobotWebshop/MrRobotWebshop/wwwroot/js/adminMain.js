(function($) {
  "use strict"; // Start of use strict

  var categoriesController = $.getScript(
    "js/categoriesAdminController.js",
    function (script, textStatus, jqXHR) {}
  );
  
  $("#manageCategories").click(function (e) { 
    e.preventDefault();

    drawHome();
  });

  $(document).on("click", "[id = addCategoryButton]", function () {
    drawAddCategoryForm();

    $("#popupForm").draggable();
  });

  $(document).on("submit", "[id = popupForm]", function (e) {
    e.preventDefault();
    var formData = $(this).serializeArray();
    var data = {CategoryName: formData[0].value};

    $.ajax({
      type: "POST",
      url: "https://localhost:44316/api/categories",
      data: data,
      success: function (response) {
        $("#formContent").empty();

        drawHome();
      },
      error: function (response) {
        $("#popupForm p").remove();
        $("#popupForm").append(
          $("<p>").attr("style", "color: red;").text("Category name already exists")
        );
      },
    });
  });

  $(document).on("click", "[id = quitCategoryButton]", function () {
    $(this.parentElement).remove();
  });

})(jQuery); // End of use strict
