$(function () {
    var pageCount = 0;
    var totalCount = 0;
    var search = null;
    var categorySelect = null;
    var installmentSelect = null;
    var priceFromSelect = null;
    var priceToSelect = null;
    var productType = 0;
    var productColor = null;

    getData(pageCount, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);

    $('.prev-page').click(function () {
        if (pageCount !== 0) {
            pageCount -= 1;
            getData(pageCount, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
        }
    });

    $('.next-page').click(function () {
        var control = totalCount - (totalCount % 10);
        if (control / 10 !== pageCount) {
            pageCount += 1;
            getData(pageCount, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
        }
    });

    $('#s').keyup(function (e) {
        let value = e.target.value;
        if (value.length > 2) {
            pageCount = 0;
            search = value;
            getData(pageCount, search);
        }
        else if (value.length === 0) {
            search = null;
            getData(0, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
        }
    });

    $('.category').click(function () {
        categorySelect = $(this).val();
        getData(0, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
    });

    $('.filterReset').click(function () {
        pageCount = 0;
        search = null;
        categorySelect = null;
        installmentSelect = null;
        priceFromSelect = null;
        priceToSelect = null;
        getData(0, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
    });

    $('.priceSelect').click(function () {
        if ($(this).val() === "0") {
            priceFromSelect = 0;
            priceToSelect = 2999;
        }
        if ($(this).val() === "1") {
            priceFromSelect = 3000;
            priceToSelect = 4999;
        }
        if ($(this).val() === "2") {
            priceFromSelect = 5000;
            priceToSelect = 7499;
        }
        if ($(this).val() === "3") {
            priceFromSelect = 7500;
            priceToSelect = 9999;
        }
        if ($(this).val() === "4") {
            priceFromSelect = 10000;
            priceToSelect = null;
        }
        getData(0, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
    });

    $('.installmentSelect').click(function () {
        installmentSelect = $(this).val();
        getData(0, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
    });

    $('.productColorSelect').click(function () {
        productColor = $(this).val();
        getData(0, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
    });

    $('.mylink').click(function () {
        $('.mylink').attr("class", "dropdown-item dropdown-toggle mylink");
        $(this).attr("class", "dropdown-item dropdown-toggle mylink active");

        pageCount = 0;
        search = null;
        categorySelect = null;
        installmentSelect = null;
        priceFromSelect = null;
        priceToSelect = null;
        productType = 0;
        productColor = null;

        productType = $(this).children('input[type="hidden"]').val();
        console.log(productType);
        getData(0, search, categorySelect, installmentSelect, priceFromSelect, priceToSelect, productType, productColor);
    });

    abp.ajax({
        type: 'GET',
        url: "/api/app/category/list",
    }).then(function (result) {
        $.each(result, function (index, value) {
            if (value.subCategory === null) {
                var clone = $('#categoryElement').clone('#category-element');
                clone.find(".category").val(value.id);
                clone.find(".category-name").html(value.name);

                clone[0].style = "display:block;"
                $('#categories').append(clone);
            }
        });
    }).catch(function () {
        alert("request failed :(");
    });
});

function getData(_skipCount, _filter, _categoryId, _installment, _priceFrom, _priceTo, _productType, _productColor) {
    $('#productWrapper > div').remove();
    abp.ajax({
        type: 'GET',
        url: "/api/app/product/async-product-with-image-list",
        data: {
            skipCount: _skipCount,
            maxResultCount: 10,
            sorting: 'creationTime desc',
            filter: _filter,
            categoryId: _categoryId,
            installment: _installment,
            priceFrom: _priceFrom,
            priceTo: _priceTo,
            productType: _productType,
            productColor: _productColor
        }
    }).then(function (result) {
        totalCount = result.totalCount;
        $.each(result.items, function (index, value) {
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