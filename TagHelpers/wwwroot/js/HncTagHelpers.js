function removeElement(array, elem) {
    var index = array.indexOf(elem.toString());
    if (index > -1) {
        array.splice(index, 1);
    }
}

function randstr(prefix) {
    return Math.random().toString(36).replace('0.', prefix || '');
}

/////////////// Start script Tags ///////////////////

var $tagsBox = $(".tags-box")
var $tagsBtnDelete = $(".tag-icon-delete")
var hncmuaTagsWrapClassName = ".hnc-tags-wrap"
var inputTagsSetValueClassName = ".input-tags-set-value"
var inputHideTagsClassName = ".input-hide"
var tagItemClassName = ".tag-item"
var $inputTagsSetValue = $(inputTagsSetValueClassName)
var prefixTagsHelper = "tags_"


$inputTagsSetValue.keyup(function (e) {
    var key = e.originalEvent.key;

    if (key != 'Enter') {
        return;
    }
    var value = $(this).val();
    setTagsInputValue(this, value)
})

function setTagsInputValue(el, val) {
    var hncmuaTagsWrap = $(el).closest(hncmuaTagsWrapClassName)
    var inputHide = hncmuaTagsWrap.find(inputHideTagsClassName)
    var inputSetValue = $(el)

    var arrValue = getInputHideValue(el)
    arrValue.push(val);

    inputHide.val(arrValue.join(","))
    inputSetValue.val("");
    renderTags(el, arrValue)

}

function renderTags(el, newArrValue) {
    var hncmuaTagsWrap = $(el).closest(hncmuaTagsWrapClassName)
    var tags = hncmuaTagsWrap.find(".tags-box")
    var inputSetValue = hncmuaTagsWrap.find(inputTagsSetValueClassName)

    // Xóa các tag đã có
    var oldTags = tags.find(".tag-item")
    oldTags.each(function () {
        $(this).remove()
    })
    // Tạo các tag theo danh sách mới
    var html = ""
    newArrValue.forEach((x,i) => {
        html += `<div class="tag-item" id="tag-item-${i}">
				    <div class="tag-value">${x}</div>
				    <div class="tag-delete-btn"><i class="far fa-times-circle" onclick="deleteTag(this)" ></i></div>
			    </div>`
    })

    inputSetValue.before(html)
}

function getInputHideValue(el) {
    var hncmuaTagsWrap = $(el).closest(hncmuaTagsWrapClassName)
    var inputHide = hncmuaTagsWrap.find(inputHideTagsClassName)
    var arrValue = inputHide.val().split(",")
    if (arrValue == null || arrValue == undefined) {
        arrValue = []
    }
    return arrValue.filter(x => x != '');
}


function deleteTag(el) {
    var hncmuaTagsWrap = $(el).closest(hncmuaTagsWrapClassName)
    var listBtnDelete = hncmuaTagsWrap.find(".fa-times-circle")
    var arrValue = getInputHideValue(el)
    var tagItemId = $(el).closest(tagItemClassName).attr("id")
    var inputHide = hncmuaTagsWrap.find(inputHideTagsClassName)
    listBtnDelete.each(function (index, el) {
        var tagItem = $(el).closest(tagItemClassName)
        var id = tagItem.attr("id")
        if (id == tagItemId) {
            tagItem.remove();
            if (index > -1) { 
                arrValue.splice(index, 1);
            }

        }
    })
    renderTags(el, arrValue);
    inputHide.val(arrValue.join(","))
}

$tagsBox.on("click", function () {
    var hncmuaTagsWrap = $(this).closest(hncmuaTagsWrapClassName)
    var inputSetValue = hncmuaTagsWrap.find(inputTagsSetValueClassName)
    inputSetValue.focus()
})

/////////////// End script Tags ///////////////////


$(document).ready(function () {

    /////////////// Start script select option ///////////////////
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

    /////////////// End script select option ///////////////////
})



