var currentPage = 1;

window.onload = (event) => {
    loadStories(currentPage);
};

function getDeltaPage(event, pageDelta) {
    event.preventDefault();
    loadStories(currentPage + pageDelta);
}

function getFirstPage(event) {
    event.preventDefault();
    loadStories(1);
}

function getLastPage(event, lastPage) {
    event.preventDefault();
    loadStories(lastPage);
}

async function loadStories(page) {

    if (page) {
        currentPage = page;
    }

    var uri = `/api/StoriesList?page=${currentPage}`;

    fetch(uri, {
        method: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(responce => responce.json())
        .then(data => listStoriesCreator(data.stories, data));
}