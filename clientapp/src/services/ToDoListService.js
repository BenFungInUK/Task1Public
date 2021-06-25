export async function getAllToDoItems() {
    const response = await fetch('/api/items');
    return await response.json();
}
