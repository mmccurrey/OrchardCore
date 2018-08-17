using System.Collections.Generic;
using System.Threading.Tasks;
using OrchardCore.Modules;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.Handlers;

namespace OrchardCore.ContentTypes.Editors
{
    public class ContentDefinitionDisplayCoordinator : IContentDefinitionDisplayHandler
    {
        private readonly IEnumerable<IContentTypeDefinitionDisplayDriver> _typeDisplayDrivers;
        private readonly IEnumerable<IContentTypePartDefinitionDisplayDriver> _typePartDisplayDrivers;
        private readonly IEnumerable<IContentPartDefinitionDisplayDriver> _partDisplayDrivers;
        private readonly IEnumerable<IContentPartFieldDefinitionDisplayDriver> _partFieldDisplayDrivers;
        private readonly IEnumerable<IContentPartFieldEditorSettingsDisplayDriver> _partFieldEditorSettingsDisplayDrivers;

        public ContentDefinitionDisplayCoordinator(
            IEnumerable<IContentTypeDefinitionDisplayDriver> typeDisplayDrivers,
            IEnumerable<IContentTypePartDefinitionDisplayDriver> typePartDisplayDrivers,
            IEnumerable<IContentPartDefinitionDisplayDriver> partDisplayDrivers,
            IEnumerable<IContentPartFieldDefinitionDisplayDriver> partFieldDisplayDrivers,
            IEnumerable<IContentPartFieldEditorSettingsDisplayDriver> partFieldEditorSettingsDisplayDrivers,
            ILogger<IContentDefinitionDisplayHandler> logger)
        {
            _partFieldDisplayDrivers = partFieldDisplayDrivers;
            _partDisplayDrivers = partDisplayDrivers;
            _typePartDisplayDrivers = typePartDisplayDrivers;
            _typeDisplayDrivers = typeDisplayDrivers;
            _partFieldEditorSettingsDisplayDrivers = partFieldEditorSettingsDisplayDrivers;
            Logger = logger;
        }

        private ILogger Logger { get; set; }

        public Task BuildTypeEditorAsync(ContentTypeDefinition model, BuildEditorContext context)
        {
            return _typeDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.BuildEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);
        }

        public Task UpdateTypeEditorAsync(ContentTypeDefinition model, UpdateTypeEditorContext context)
        {
            return _typeDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.UpdateEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);
        }

        public Task BuildTypePartEditorAsync(ContentTypePartDefinition model, BuildEditorContext context)
        {
            return _typePartDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.BuildEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);
        }

        public Task UpdateTypePartEditorAsync(ContentTypePartDefinition model, UpdateTypePartEditorContext context)
        {
            return _typePartDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.UpdateEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);
        }

        public Task BuildPartEditorAsync(ContentPartDefinition model, BuildEditorContext context)
        {
            return _partDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.BuildEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);
        }

        public Task UpdatePartEditorAsync(ContentPartDefinition model, UpdatePartEditorContext context)
        {
            return _partDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.UpdateEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);
        }

        public async Task BuildPartFieldEditorAsync(ContentPartFieldDefinition model, BuildEditorContext context)
        {
            await _partFieldDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.BuildEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);

            await _partFieldEditorSettingsDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.BuildEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);
        }

        public async Task UpdatePartFieldEditorAsync(ContentPartFieldDefinition model, UpdatePartFieldEditorContext context)
        {
            await _partFieldDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.UpdateEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);

            await _partFieldEditorSettingsDisplayDrivers.InvokeAsync(async contentDisplay =>
            {
                var result = await contentDisplay.UpdateEditorAsync(model, context);
                if (result != null)
                    await result.ApplyAsync(context);
            }, Logger);
        }
    }
}