var currentPage = 1;
var currentType = 'Котка';
var currentOrderType = 'Id';
var currOrderDescAsc = 'desc';

//Add orderByEvents
document.getElementById('dateOrder').addEventListener("click", function () { loadAnimals(currentPage, currentType, 'Id'); });
document.getElementById('likeOrder').addEventListener("click", function () { loadAnimals(currentPage, currentType, 'Likes'); });
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

    loadAnimals(currentPage, currentType, currentOrderType, currOrderDescAsc);
}

async function loadAnimals(page, type, orderType, orderDescAsc) {

    if (page) {
        currentPage = page;
    }

    if (type) {
        currentType = type;
    }

    if (orderType) {
        currentOrderType = orderType;
    }

    if (orderDescAsc) {
        currOrderDescAsc = orderDescAsc;
    }

    var uri = `/api/PetPostList?info=${currentType}&page=${currentPage}&order=${currentOrderType}&orderType=${currOrderDescAsc}`;

    fetch(uri, {
        method: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(responce => responce.json())
        .then(data => listPostCreatorVertical(data.animals, data, currentType));
}


