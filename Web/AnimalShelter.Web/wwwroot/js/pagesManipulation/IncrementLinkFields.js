var hiddenCounter = document.getElementById("count");
var counter = hiddenCounter.getAttribute("data-attribute");

function AddLinks() {
    //for linkName
    let LinkNameDiv = document.createElement('div');
    LinkNameDiv.classList.add("cell-sm-6");

    let divNameForm = document.createElement('div');
    divNameForm.classList.add("form-group");

    let inputName = document.createElement('input');
    inputName.classList.add("form-control");
    inputName.setAttribute("type", "text");
    inputName.setAttribute("name", `OrganisationLinks[${counter}].LinkName`);

    let labelName = document.createElement('label');
    labelName.setAttribute("for", "LinkName");
    let labelText = document.createTextNode("Име на линка");
    labelName.appendChild(labelText);

    divNameForm.appendChild(inputName);
    divNameForm.appendChild(labelName);
    LinkNameDiv.appendChild(divNameForm);

    //for link
    let LinkDiv = document.createElement('div');
    LinkDiv.classList.add("cell-sm-6");

    let divLinkForm = document.createElement('div');
    divLinkForm.classList.add("form-group");

    let inputLink = document.createElement('input');
    inputLink.classList.add("form-control");
    inputLink.setAttribute("type", "text");
    inputLink.setAttribute("name", `OrganisationLinks[${counter}].LinkHref`);

    let labelLink = document.createElement('label');
    labelLink.setAttribute("for", "LinkHref");
    let labelLinkText = document.createTextNode("Линк");
    labelLink.appendChild(labelLinkText);

    divLinkForm.appendChild(inputLink);
    divLinkForm.appendChild(labelLink);
    LinkDiv.appendChild(divLinkForm);

    let form = document.getElementById("OrganisationForm");
    form.appendChild(LinkNameDiv);
    form.appendChild(LinkDiv);

    counter++;
}

function RemoveLinks() {

}