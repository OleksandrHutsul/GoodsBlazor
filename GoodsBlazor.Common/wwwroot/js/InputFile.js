async function openFilePicker() {
    try {
        const [fileHandle] = await window.showOpenFilePicker({
            types: [{ description: 'Images', accept: { 'image/*': ['.png', '.jpg', '.jpeg'] } }]
        });

        const file = await fileHandle.getFile();
        if (file && (file.type === 'image/png' || file.type === 'image/jpeg')) {
            const blob = new Blob([await file.arrayBuffer()], { type: file.type });
            const objectUrl = URL.createObjectURL(blob);

            const arrayBuffer = await blob.arrayBuffer();
            const uint8Array = new Uint8Array(arrayBuffer);

            return {
                objectUrl: objectUrl,
                byteArray: Array.from(uint8Array)
            };
        }
    } catch (error) {
        console.error('File selection cancelled or failed:', error);
    }
    return null;
}
