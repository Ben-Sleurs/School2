
//elem: clicked button
//divToShow: Div id name that has to get block display
function changeProject(elem,divToShow){
    let activeButton = elem.parentElement.querySelector(".button-active");
    activeButton.classList.remove("button-active");
    elem.classList.add("button-active");

    let shownDiv = elem.parentElement.parentElement.querySelector(".wpl-div-show");
    console.log(shownDiv);
    shownDiv.classList.replace("wpl-div-show","wpl-div-hide");
    document.getElementById(divToShow).classList.replace("wpl-div-hide","wpl-div-show");
}


