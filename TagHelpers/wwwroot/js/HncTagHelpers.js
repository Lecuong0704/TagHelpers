$(document).ready(function () {
    var $selected = $(".selected");
    var $selectItem = $(".select-item");
    var hncSelectWrapClassName = ".hnc-select-wrap";
    var hncSelectBoxClassName = ".hnc-select-box";
    var inputHideClassName = ".input-hide";
    var selectOptionOpenId = "";
    var classShowOptions = "show-options"

    $selected.on("click", function () {
        var hncSelectWrap = $(this).closest(hncSelectWrapClassName);
        var selectBox = hncSelectWrap.find(hncSelectBoxClassName);
        var id = hncSelectWrap.attr("id");
        
        if (selectBox.hasClass(classShowOptions)) {
            selectBox.removeClass(classShowOptions)
            selectOptionOpenId = "";
        } else {
            selectBox.addClass(classShowOptions)
            if (selectOptionOpenId != id && selectOptionOpenId != null && selectOptionOpenId != undefined && selectOptionOpenId != "") {
                $(`#${selectOptionOpenId}`).find(hncSelectBoxClassName).removeClass(classShowOptions)
            }
            selectOptionOpenId = hncSelectWrap.attr("id")
        }
    })

    $selectItem.on("click", function () {
        var hncSelectWrap = $(this).closest(hncSelectWrapClassName);
        var selectBox = hncSelectWrap.find(hncSelectBoxClassName);
        var isMultiSelect = hncSelectWrap.hasClass("multi-select");
        setValueSelected(this)
        if (!isMultiSelect) {
            selectBox.removeClass(classShowOptions)
            selectOptionOpenId = "";
        }
    })

    window.addEventListener("click", function (event) {
        var $target = $(event.target);
        var hncSelectWrap = $target.closest(hncSelectWrapClassName);
        if (!hncSelectWrap.length && selectOptionOpenId != null && selectOptionOpenId != undefined && selectOptionOpenId != "") {
            $(`#${selectOptionOpenId}`).find(hncSelectBoxClassName).removeClass(classShowOptions)
            selectOptionOpenId = "";
        }
    })

    function setValueSelected(el) {
        var hncSelectWrap = $(el).closest(hncSelectWrapClassName);
        var selectBox = hncSelectWrap.find(hncSelectBoxClassName);
        var input = selectBox.find(inputHideClassName)
        var itemValue = $(el).data("value")
        var selected = selectBox.find(".value")
        var isMultiSelect = hncSelectWrap.hasClass("multi-select");

        var arrValue = [];

        if (isMultiSelect) {
            var arrValue = input.val().split(",")
            if (arrValue == null || arrValue == undefined) {
                arrValue = []
            }
        }

        var newArrValue = arrValue.filter(x => x != '');
        var isHas = newArrValue.some(x => {
            return x == itemValue
        });

        if (isHas) {
            removeElement(newArrValue, itemValue)

        } else {
            newArrValue.push(itemValue)
        }

        var items = hncSelectWrap.find(".select-item")
        var arrText = []

        items.each(function () {
            var val = $(this).data("value");
            var text = $(this).data("text");
            var isSelected = false;
            isSelected = newArrValue.some(x => {
                return x == val
            });
            if (isSelected) {
                $(this).addClass("select-item__selected")
                arrText.push(text)
            } else {
                $(this).removeClass("select-item__selected")
            }
        })

        input.val(newArrValue.join(","))
        var newText = selected.data("placeholder")
        if (arrText.length > 0) {
            newText = arrText.join(', ');
            selected.removeClass("no-value")
        } else {
            selected.addClass("no-value")

        }
        selected.text(newText)
    }

    function removeElement(array, elem) {
        var index = array.indexOf(elem.toString());
        if (index > -1) {
            array.splice(index, 1);
        }
    }

})