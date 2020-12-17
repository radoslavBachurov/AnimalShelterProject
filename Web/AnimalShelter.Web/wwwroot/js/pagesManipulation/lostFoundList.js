var currentPage = 1;
var currentCategory = 'Всички';
var currentOrderType = 'Id';
var currOrderDescAsc = 'desc';

//Add orderByEvents
document.getElementById('dateOrder').addEventListener("click", function () { loadAnimals(currentPage, currentCategory, 'Id'); });
document.getElementById('likeOrder').addEventListener("click", function () { loadAnimals(currentPage, currentCategory, 'Likes'); });
document.getElementById('ascOrder').addEventListener("click", function () { changeOrderDescAsc() });


function getDeltaPage(event, pageDelta) {
    event.preventDefault();
    loadAnimals(currentPage + pageDelta);
}

function getFirstPage(event) {
    event.preventDefault();
    loadAnimals(1);
}

function getLastPage(event, lastPage) {
    event.preventDefault();
    loadAnimals(lastPage);
}

function changeOrderDescAsc() {
    if (currOrderDescAsc == 'desc') {
        currOrderDescAsc = 'asc';
    }
    else {
        currOrderDescAsc = 'desc'
    }

    loadAnimals(currentPage, currentCategory, currentOrderType, currOrderDescAsc);
}

async function loadAnimals(page, category, orderType, orderDescAsc) {

    if (page) {
        currentPage = page;
    }

    if (category) {
        currentCategory = category;
    }

    if (orderType) {
        currentOrderType = orderType;
    }

    if (orderDescAsc) {
        currOrderDescAsc = orderDescAsc;
    }

    var uri = `/api/LostFoundList?category=${currentCategory}&page=${currentPage}&order=${currentOrderType}&orderType=${currOrderDescAsc}`;

    fetch(uri, {
        method: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(responce => responce.json())
        .then(data => listCreator(data.animals, data));
}


