$(function () {
    var _categoryId = document.getElementById("categoryId").value;
    getData(_categoryId);
    function getData(categoryId) {
        $('#productWrapper > div').remove();
        abp.ajax({
            type: 'GET',
            url: "/api/app/category/" + categoryId +"/list-with-detail"
        }).then(function (result) {
            $.each(result.products, function (index, value) {
                var clone = $('#productElement').clone('#element');
                clone.find(".product-image").attr("src", value.images[0].image);
                clone.find(".product-title").html(value.title);
                clone.find("a").attr("href", "/Product/ProductDetail/?id=" + value.id);
                if (value.discount === true) {

                    clone.find(".product-discount-amount-visible").css("display", "inline-block");
                    clone.find(".installment").css("display", "inline-block");

                    if (value.installment !== 0) {
                        clone.find(".installment").html("x" + value.installment + " Ay");
                        var installmentPrice = value.discountPrice / value.installment;
                        clone.find(".product-discount-amount").html("₺" + value.price);
                        clone.find(".product-amount").html("₺" + installmentPrice.toFixed(2));
                    }
                    else {
                        clone.find(".installment").css("display", "none");

                        clone.find(".product-discount-amount").html("₺" + value.price);
                        clone.find(".product-amount").html("₺" + value.discountPrice);
                    }
                }
                else {

                    clone.find(".product-discount-amount-visible").css("display", "none");


                    if (value.installment !== 0) {
                        clone.find(".installment").html("x" + value.installment + " Ay");
                        var installmentPrice = value.price / value.installment;
                        clone.find(".product-amount").html("₺" + installmentPrice.toFixed(2));
                    }
                    else {
                        clone.find(".installment").css("display", "none");
                        clone.find(".product-amount").html("₺" + value.price);
                    }
                }
                clone[0].style = "display:block;"
                $('#productWrapper').append(clone);
            });
        }).catch(function () {
            alert("request failed :(");
        });
    }
});