function copyToClipboard() {
    const joke = document.getElementById("joke").textContent;
    const textArea = document.createElement("textarea");
    textArea.value = joke;
    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand("copy");
    document.body.removeChild(textArea);
    alert("Joke copied to clipboard: " + joke);
}