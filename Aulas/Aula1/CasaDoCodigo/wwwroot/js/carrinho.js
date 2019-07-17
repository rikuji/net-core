
class Carrinho {

    clickIncremento(btn) {
        let data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);
    }

    clickDecremento(btn) {
        let data = this.getData(btn);
        data.Quantidade--;
        this.postQuantidade(data);
    } 
    updateQuantidade(input) {
        let data = this.getData(input);
        this.postQuantidade(data);
    }
    getData(element) {
        var linhaItem = $(element).parents('[item-id]');
        var itemId = $(linhaItem).attr('item-id');
        var novaQtde = $(linhaItem).find('input').val();

        var data = {
            Id: itemId,
            Quantidade: novaQtde
        };

        return {
            Id: itemId,
            Quantidade: novaQtde
        };
    }

    postQuantidade(data) {
        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        });
    }
}
var carrinho = new Carrinho();


