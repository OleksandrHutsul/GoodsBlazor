window.openFilePicker = async function () {
    console.log("openFilePicker loaded!");
    try {
        const [fileHandle] = await window.showOpenFilePicker({
            types: [{
                description: 'Images',
                accept: { 'image/*': ['.png', '.jpg', '.jpeg'] }
            }]
        });

        const file = await fileHandle.getFile();
        if (file && (file.type === 'image/png' || file.type === 'image/jpeg')) {
            const reader = new FileReader();
            return new Promise((resolve) => {
                reader.onload = (e) => {
                    resolve(e.target.result.split(',')[1]);
                };
                reader.readAsDataURL(file);
            });
        }
    } catch (error) {
        console.error('File selection cancelled or failed:', error);
    }
    return null;
};
