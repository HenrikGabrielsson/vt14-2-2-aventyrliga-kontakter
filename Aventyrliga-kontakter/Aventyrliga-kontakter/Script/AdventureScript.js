function confirmDelete()
{
    return confirm("Är du säker på att du vill ta bort kontakten?");
}

function closeWindow()
{
    var button = document.getElementById("CloseButton");

    button.addEventListener("click", function ()
    {
        var window = button.parentNode;
        window.parentNode.removeChild(window);
    }   
    ,false)

}